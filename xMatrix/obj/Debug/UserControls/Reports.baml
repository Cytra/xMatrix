   M S B A M L   `   `   `  ����B  >xMatrix, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:xMatrix.UserControlsxMatrix.UserControls  5      "    xMatrix.UserControls.Reports   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .local"clr-namespace:xMatrix.UserControls  5      .��5
      �   � ColumnDefinitions  5   
   �� 5      $��* �6   6+   �� 5      $��10 �6   6,   �� 5      $��* �6   6+   �� 5      $��10 �6   6,   �� 5      $��* �6   6+    � RowDefinitions 5   
   �� 5      $��50 �6   6*   �� 5      $��* �6   6)   .+�5   
   �� $��0��5      $��0��5       $   MaterialDesignHeadline5TextBlock#��[  5      $��Center=�5      Level One Report5      &   System.Windows.Controls.DataGrid  5   
   $��1��5       $��0��5!         CanUserSortColumns .    CanUserAddRows .     AutoGenerateColumns .  ��5"      �� *LevelOneReport+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�   Columns 5'      1  +MaterialDesignThemes.Wpf.DataGridTextColumn  5(      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��5*      	   
IsReadOnly	 .  5 1MaterialDesignDataGridTextColumnPopupEditingStyle1  +System.Windows.Controls.DataGridBoundColumn
   EditingElementStyle#
 [ 5,         Binding 5)      �� *Department.Name+5,        5-      $ 	Goal Type��5/      	 . #
 [ 51       5.      �� *	Goal.Name+51        52      $ 	Goal Type��54      	 . #
 [ 56       53      �� *Goal.GoalType+56      58      �� 5:   
   $��0��5;      $��2��5<      #��[  5=      $��Center=�5>      Level Two Report5@        5B   
   $��1��5C      $��2��5D       .  .   .  ��5E      �� *LevelTwoReport+$ TwoWay=�$ PropertyChanged=� 5J        5K      $
 Name��5M      	 . #
 [ 5O       5L      �� *Department.Name+5O        5P      $ 	Goal Type��5R      	 . #
 [ 5T       5Q      �� *	Goal.Name+5T        5U      $ 	Goal Type��5W      	 . #
 [ 5Y       5V      �� *Goal.GoalType+5Y      5[      �� 5]   
   $��0��5^      $��4��5_      #��[  5`      $��Center=�5a      Level Three Report5c        5e   
   $��1��5f      $��4��5g       .  .   .  ��5h      �� *LevelThreeReport+$ TwoWay=�$ PropertyChanged=� 5m        5n      $
 Name��5p      	 . #
 [ 5r       5o      �� *Department.Name+5r        5s      $ 	Goal Type��5u      	 . #
 [ 5w       5t      �� *	Goal.Name+5w        5x      $ 	Goal Type��5z      	 . #
 [ 5|       5y      �� *Goal.GoalType+5|      5~      5�      5�      