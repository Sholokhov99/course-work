   M S B A M L   `   `   `  ����D  @VideoPlay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:VideoPlay	VideoPlay  5   	       VideoPlay.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:VideoPlay     5   	   $	��528��5      $	��826��6'   $	��528��63   $	��826��6C     � WindowStyle$  ThreeDBorderWindow=�6R    � Title$ Видеоплеер��6s   .��5      � ���	#FF646464.+�5
   
   r� -   5         r�Name$ mediaContent��5
      $��Center=�6-   $��0,0,0,48q�6H    r� LoadedBehavior$ Manual=�6Z   ]� 5   
   $��Stretch=�6   $��43��63   ��. $��Bottom=�6U   
���   �$��2��6�   .+�5      �� -   5         ��Name$ 
pauseMedia��5      $�� ��6)   $��30��64   $��30��6@   $
��Left=�6K   $��10,0,0,0q�6f    � Cursor$
 Hand��6�   ��5      �� 5       �� ImageSource$ Media-Pause-128.png��6!   5      �� 5      -   5        $ 	playMedia��5      $�� ��6(   $��30��63   $��30��6?   $
��Left=�6J   $��10,0,0,0q�6e    J� RenderTransformOrigin$	 0,0&�6�   $��0,1,1,1q�6�   $
 Hand��6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      ��5      �� 5      $ Media-Play-128.png��6!   5      5      5      5      