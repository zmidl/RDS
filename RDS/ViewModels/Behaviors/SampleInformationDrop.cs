using System.Windows;
using System.Windows.Interactivity;
using System.Linq;
using RDS.Models;
using System.Collections.Generic;
using System.Collections;
using RDS.ViewModels.Descriptions;
using RDS.ViewModels.Common;
using System;

namespace RDS.ViewModels.Behaviors
{
	public class SampleInformationDrop : Behavior<UIElement>
	{
		public SampleViewModel ViewModel
		{
			get { return (SampleViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(nameof(ViewModel), typeof(SampleViewModel), typeof(SampleInformationDrop), new PropertyMetadata(null));


		public SampleInformationDrop()
		{

		}

		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.Drop += AssociatedObject_Drop;
			this.AssociatedObject.DragEnter += AssociatedObject_DragEnter;
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.Drop -= AssociatedObject_Drop;
			this.AssociatedObject.DragEnter -= AssociatedObject_DragEnter;
		}

		private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
		{
			//if (this.AssociatedObject.GetType().Equals(typeof(RDSCL.SingleTube)))
			//{
			//	var currentTube = this.AssociatedObject as RDSCL.SingleTube;
			//	currentTube.ExcircleColor = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
			//}
		}

		private void AssociatedObject_Drop(object sender, DragEventArgs e)
		{
			// 得到拖拽来的样品信息
			var draggedDatas = ((IList)e.Data.GetData(e.Data.GetFormats()[0])).Cast<SampleInformation>().ToList();

			// 得到需要实验的样品信息
			var experimentInformations = this.GetExperimentInformations(draggedDatas);

			if(experimentInformations.Count == 0)
			{
				MessageBox.Show("所选的样本信息已有对应的孔位，请重新布样。");
			}
			else
			{
				// 根据容器类型判断是否需要路由传递事件
				if (this.AssociatedObject.GetType().Equals(typeof(RDSCL.SingleTube)))
				{
					var currentTube = this.AssociatedObject as RDSCL.SingleTube;

					var experimentSampleTube = this.ViewModel.SampleDescriptions.FirstOrDefault(o => o.HoleName == currentTube.Name && o.State==Sampling.NoSample);

					if (experimentSampleTube == null) MessageBox.Show("当前样本管已有样本信息，请重新布样。");
					else
					{
						experimentInformations[0].HoleName = experimentSampleTube.HoleName;
						experimentSampleTube.State = Sampling.NormalSampling;
					}
					e.Handled = true;
				}
				else
				{
					// 得到需要实验的样品空管
					var experimentSampleTubes = this.GetExperimentSampleTubes();

					if (experimentSampleTubes.Count == 0) MessageBox.Show("没有可用于样本信息的孔位，请重新布样。");
					else
					{
						var experimentCount = Math.Min(experimentInformations.Count, experimentSampleTubes.Count);
						for (int i = 0; i < experimentCount; i++)
						{
							experimentInformations[i].HoleName = experimentSampleTubes[i].HoleName;
							experimentSampleTubes[i].State = Sampling.NormalSampling;
						}
					}
				}
			}
		}

		private List<SampleInformation> GetExperimentInformations(List<SampleInformation> draggedDatas)
		{
			// 暂定所有的无孔位的样本信息都是实验信息
			var experimentSampleInformations = this.ViewModel.SampleInformations.Where(o => o.HoleName == string.Empty).ToList();

			// 如用户有选择指定的样本信息则这些信息定为待实验的样品信息
			if (draggedDatas.Count > 0) experimentSampleInformations = draggedDatas.Where(o => o.HoleName == string.Empty).ToList();
			
			// 返回待实验的样本信息
			return experimentSampleInformations;
		}

		private List<SampleDescription> GetExperimentSampleTubes()
		{
			// 暂定所有的空试管为实验样本管
			var experimentSampleTubes = this.ViewModel.SampleDescriptions.Where(o => o.State == Sampling.NoSample).ToList();

			// 筛选所有被选中的试管
			var selectedSampleTubes = this.ViewModel.SampleDescriptions.Where(o => o.IsSelected == true).ToList();

			// 如果有选中的试管那么筛选其中空的试管为实验试管
			if (selectedSampleTubes.Count > 0) experimentSampleTubes = selectedSampleTubes.Where(o => o.State == Sampling.NoSample).ToList();

			// 返回筛选出的实验管
			return experimentSampleTubes;
		}
	}
}
