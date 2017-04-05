﻿using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace RDSCL
{
	/// <summary>
	/// SampleTube.xaml 的交互逻辑
	/// </summary>
	public partial class SampleRack : UserControl
	{
		public IEnumerable SamplesContentColor
		{
			get { return (IEnumerable)GetValue(SamplesContentColorProperty); }
			set { SetValue(SamplesContentColorProperty, value); }
		}
		public static readonly DependencyProperty SamplesContentColorProperty =
			DependencyProperty.Register(nameof(SamplesContentColor), typeof(IEnumerable), typeof(SampleRack), new PropertyMetadata(null));

		public SampleRackState SampleRackState
		{
			get { return (SampleRackState)GetValue(SampleRackStateProperty); }
			set { SetValue(SampleRackStateProperty, value); }
		}
		public static readonly DependencyProperty SampleRackStateProperty =
			DependencyProperty.Register(nameof(SampleRackState), typeof(SampleRackState), typeof(SampleRack), new PropertyMetadata(SampleRackState.AlreadySample, new PropertyChangedCallback(Callback_SampleRackState)));

		private static void Callback_SampleRackState(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var sampleRack = d as SampleRack;
			DoubleAnimation flashAnimation = new DoubleAnimation(0d, 1d, new Duration(System.TimeSpan.FromSeconds(1)));
			flashAnimation.Completed += (sender, eventArgs) =>
			{
				if (sampleRack.SampleRackState==SampleRackState.PrepareSample) sampleRack.BeginAnimation(UIElement.OpacityProperty, flashAnimation);
			};
			switch (sampleRack.SampleRackState)
			{
				case SampleRackState.NotSample:
				{
					sampleRack.Visibility = Visibility.Collapsed;
					break;
				}
				case SampleRackState.PrepareSample:
				{
					sampleRack.Visibility = Visibility.Visible;
					sampleRack.BeginAnimation(UIElement.OpacityProperty, flashAnimation);
					break;
				}
				case SampleRackState.AlreadySample:
				{
					sampleRack.Visibility = Visibility.Visible;
					break;
				}
				default: break;
			}
		}

		public SampleRack()
		{
			InitializeComponent();
		}
	}

	public enum SampleRackState
	{
		NotSample = 0,
		PrepareSample = 1,
		AlreadySample = 2
	}
}