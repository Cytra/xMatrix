   M S B A M L   `   `   `  ����B  >xMatrix, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:xMatrix.UserControlsxMatrix.UserControls  5      $    xMatrix.UserControls.UserInput   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .local"clr-namespace:xMatrix.UserControls  5      .��5
      �   � ColumnDefinitions  5   
   �� 5      $��* �6   6+   �� 5      $��5* �6   6,   �� 5      $��* �6   6+   �� 5      $��5* �6   6,   �� 5      $��* �6   6+   �� 5      $��5* �6   6,   �� 5      $��* �6   6+    � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $��5* �6   6*   �� 5      $��* �6   6)   �� 5      $��5* �6   6*   �� 5      $��* �6   6)   �� 5      $��5* �6   6*   �� 5      $��* �6   6)   �� 5      $	��10* �6   6+   �� 5      $��* �6   6)   �� 5      $��* �6   6)   �� 5"      $��* �6   6)   .+�5&   
   �� $��1��5'      $��1��5(      $��5��5)       $   MaterialDesignHeadline4TextBlock#��[  5*      $��Center=�5+      Add New Goals5-      �� 5/   
   $��3��50      $��1��6   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 	Goal name��51       % !MaterialDesignFloatingHintTextBox#��[ 52      $��Center=�53      ��54      �� *NewGoalName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 56   
   $��3��57      $��3��6   $ 	Goal Type��58       �� SelectedItem 59      �� *NewGoalType+��5:      �� *	GoalTypes+$ TwoWay=�$ PropertyChanged=�5;      �� 5=   
   $��3��5>      $��5��6     MaterialDesignRaisedButton#��[ 5?       � ToolTip$/ )Resource name: MaterialDesignRaisedButton��5@      ��5A      �� *
AddNewGoal+.��6,   ADD5C      �� 5E   
   $��5��5F      $��1��5G      $��5��5H      #��[  5I      $��Center=�5J      Modify Goals5L      &   System.Windows.Controls.DataGrid  5O   
   $��7��5P      $��1��5Q      $��1��5R         CanUserSortColumns .    CanUserAddRows .  	   AutoGenerateColumns	 .  ��5S      �� *Goals+$ TwoWay=�$ PropertyChanged=� 5W      �� *SelectedGoal+
   Columns
 5X      1  +MaterialDesignThemes.Wpf.DataGridTextColumn  5Y      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��5[         
IsReadOnly .  5 1MaterialDesignDataGridTextColumnPopupEditingStyle1  +System.Windows.Controls.DataGridBoundColumn   EditingElementStyle# [ 5]         Binding 5Z      �� *Name+5]        5^      $ 	Goal Type��5`       . # [ 5b       5_      �� *
GoalType+5b      5d      �� 5f   
   $��9��5g      $��1��6   #��[ 5h      $/ )Resource name: MaterialDesignRaisedButton��5i      ��5j      �� *
DeleteGoal+.��6,   DELETE GOAL5l        5n   
   $��7��5o      $��3��5p      $��1��5q       .  .  	 .  ��5r      �� *AllRelatedGoals+$ TwoWay=�$ PropertyChanged=� 5v      �� *SelectedRelatedGoal+
 5w        5x      $
 Name��5z       . # [ 5|       5y      �� *Name+5|        5}      $ 	Goal Type��5       . # [ 5�       5~      �� *
GoalType+5�      5�      �� 5�   
   $��9��5�      $��3��6   #��[ 5�      $/ )Resource name: MaterialDesignRaisedButton��5�      ��5�      �� *AddRelatedGoal+.��60   ADD RELATION5�        5�   
   $��7��5�      $��5��5�      $��1��5�       .  .  	 .  ��5�      �� *RelatedGoals+$ TwoWay=�$ PropertyChanged=� 5�      �� *SelectedRemoveRelatedGoal+
 5�        5�      $
 Name��5�       . # [ 5�       5�      �� *Name+5�        5�      $ 	Goal Type��5�       . # [ 5�       5�      �� *
GoalType+5�      5�      �� 5�   
   $��9��5�      $��5��6   #��[ 5�      $/ )Resource name: MaterialDesignRaisedButton��5�      ��5�      �� *RemoveRelatedGoal+.��63   REMOVE RELATION5�      5�      5�      