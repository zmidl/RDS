   M S B A M L   `   `   `  ����  RDS    RDS.Views.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � WindowState$  	Maximized=�5   	    � Title$ 
MainWindow��5   	   $
��1000��6   $
��1900��6+    J� InputBindings 5      �� 5   
    �� 	Modifiers$ Controlf�6   
 �� Key$ M��6*    �� Command 62   �� *
ReturnHome+ � 	Resources 5
         
ReturnHome �� Text��%/   &	        �� 5   
   $ return home��6-   5       J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5        	   Command#	 [  5      5      .��5      �� .+�5   
   (� 
 (� Stretch$

 Fill=�6   .��5      �� -   5         ��Name$ StackPanel_Animation��5       �� Orientation$ Vertical=�67   $
��Left=�6O   .+�5      �� $��模拟动画��6   $	��120(�6(   $��Center=�67   $��Center=�6T   �� 5      -   5         ��Name$ TextBlock_count��5      $��12��65   $	��500(�6?   $��Center=�6N   $��Center=�6k   5      5      �� 5   
   -   5         ��Name$ Page_1��5      
���  ��$y�Hidden=�62   $
��1892��6F   $	��992��6S   5      5!      