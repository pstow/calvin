   M S B A M L   `   `   `  ���� A  =Calvin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Calvin.TouchCalvin.Touch  %    Calvin.AdminManager.LogOnWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -Keypadclr-namespace:Calvin.Touch     -      styleWindowGray#��[    � Title$  LogOnWindow��$	��320��$	��532�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � 	Resources   lclStyleTbxEntry �� 
TargetType �� Property �� Value  lclStyleLbl1  styleLblUser �� BasedOn  lclStyleLbl2  styleLblBoldL  lclStyleLbl3  lclStyleColon��%�  &	       &	 �     7  &	 E    7  &	 �    7  &	 )    �� " ��.���  � ��$ 20(���  � ��$
 Left=���  � ��$ Bottom=���  � ��$ Center=���  � ��$	 290����  � ��$ 40����  � ��$
 Bold"���  � ��$ 0,0,0,3q���  � ��$ 5,0,0,0q��� 8   " ��.���  � ��$	 150����  � ��$ Right=���  � ��$ Right=��� 8   " ��.���  � ��$ 20(���  � ��$	 240����  � ��$
 Left=���  � ��$ 8,-2,0,2q���  � ��
 ��  ��� 8   " ��.���  � ��$	 115����  � ��$ 18(���  � ��$ Center=��� " ��.���  � ��$ 0q���  � ��$ :����  � ��$ 18(���  � ��$
 Bold"���  � ��$ Center=�.����   styleBorder#��[ ._�� 	 � RowDefinitions	 �� $��50 ��� $��95 ��� $	��115 ��� $��50 �.+�� $��0��
 � ColumnDefinitions
 �� $��* ��� $��1 �.+��� -    ��Name$ lblStatus_copy�� 	 styleLblHeader#��[	 $��40��$��22(�$��User Log On���� -   $ lblFocus��$��1��$��1��|�. �� $��1��#��[ $	��516��._�� $��1��	 �� $��5 ��� $��40 ��� $��40 �
 �� $	��150 ��� $��8 ��� $��* ��� $	��100 �.+��� $��1��$��0��#��[ $��Current User���� $��1��$��1��#��[ �� -   $ 
lblCurrent��$��1��$��2��#��[ $��18(��� -   $ labDate��$��2��$��0��#��[ $��	Date/Time���� $��2��$��1��#��[ �� -   $ lblDate��$��2��$��2��#��[ $��16(��� -    ��Name$ btnLock��$��1��$��3��$��2�� 
 styleBtnStart#��[
 $��55��$��80���� $��2��#��[ $	��516��._�� 	 �� $��50 ��� $��50 �
 �� $	��120 ��� $��10 ��� $	��300 ��� $��50 �.+��� -   $ labUser��$��0��$��0��#��[ $
��User���� $��0��$��1��#��[ "   Calvin.Touch.TextBoxKeyboard  -	     Name$ kpdUser��$��0��$��2��#��[    KBMode$ AlphaNumeric=��� -
   $ labPassword��$��1��$��0��#��[ $��Password���� $��1��$��1��#��[   -   $ kpdPassword��$��1��$��2��#��[ $ Password=�� $��3��
 �� $��20 ��� $	��125 ��� $��* ��� $	��125 ��� $��* ��� $	��125 ��� $��20 �.+��� -   $ btnLogOn��$��1��#��[
 $��Log On���� -   $ 	btnLogOff��$��3��#��[
 $��Log Off���� -   $ btnClose��$��5��#��[
 $��Close��