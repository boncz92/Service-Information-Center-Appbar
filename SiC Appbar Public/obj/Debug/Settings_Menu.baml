   M S B A M L   `   `   `  ����E  ASiC Appbar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:SiC_Appbar
SiC_Appbar  5   	       SiC_Appbar.Settings_Menu   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   $localclr-namespace:SiC_Appbar  5   	   -   5           MaterialDesignBody#���   5   	     MaterialDesignPaper#���  5   	   $��Medium"�5	   	   $��14(�5
   	     � WindowStyle$  
ToolWindow=�5   	    � 
ResizeMode$ NoResize=�5   	    � Title$ 	 Settings��5   	   $	��500��5   	   $	��500��5   	   ��5   	   :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .��5      �  � RowDefinitions 5   
   �� 5      $��75 �6   �� 5      .+�5   
   �� $��0��6   $(��"Active Directory Password Settings��6   $
��Left=�6J   $	��Top=�6e   	� 5   
   -   5        $��PwSettings_Random��5      $��0��6/    	� 	GroupName$ 	PwSetting��6<   $��Random��6R   $
��Left=�6c   $	��Top=�6~   $��	10,30,0,0q�6�   	� 5   
   -   5        $��PwSettings_Static��5      $��0��6/   $ 	PwSetting��6<   $��Static��6R   $
��Left=�6c   $	��Top=�6~   $��	90,30,0,0q�6�   �� 5   
   -   5        $��PwSettings_StaticString��5   )   $��-10,-7,0,-10q�6   $
��Left=�6H   $��24��6c   $��
170,25,0,0q�6o    �� TextWrapping$ NoWrap=�6�   $	��Top=�6�   $	��192��6�    * &MaterialDesignOutlinedTextFieldTextBox#��[ 6�   $��14(�6�   �� 5   
   $��1��6   $��Appbar Location Settings��6   $
��Left=�6@   $	��Top=�6[   $��0,10,0,0q�6s   	� 5   
   -   5        $��AppBarLocSettings_Top��5      $��1��63   $ 	AppBarLoc��6@   $	��Top��6V   $
��Left=�6d   $	��Top=�6   $��	10,40,0,0q�6�   	� 5    
   -   5        $��AppBarLocSettings_Bottom��5       $��1��66   $ 	AppBarLoc��6C   $��Bottom��6Y   $
��Left=�6j   $	��Top=�6�   $��	90,40,0,0q�6�   5#      5$      