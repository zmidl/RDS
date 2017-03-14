using RDS.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace RDS.ViewModels.Behaviors
{
	class SampleInformationDrag : Behavior<UIElement>
	{

		public object DraggedData
		{
			get { return (object)GetValue(DragedDataProperty); }
			set { SetValue(DragedDataProperty, value); }
		}
		public static readonly DependencyProperty DragedDataProperty =
			DependencyProperty.Register(nameof(DraggedData), typeof(object), typeof(SampleInformationDrag), new PropertyMetadata(null));

		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
		}

		private void AssociatedObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (e.RightButton == MouseButtonState.Pressed)
			{
				var property = typeof(System.Windows.Controls.DataGrid).GetField
					($"_isDraggingSelection",
					System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.IgnoreCase);
				if (property != null) property.SetValue(this.AssociatedObject, false);
				
				DragDrop.DoDragDrop(this.AssociatedObject, this.DraggedData, DragDropEffects.Copy);
			}
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
		}
	}
}
