   M S B A M L   `   `   `  ���� 
  Calvin(    "Calvin.ReportsManager.ReportWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     $	��768��$
��1366��$��18(�  � WindowStyle$
  None=� � 
ResizeMode$ NoResize=� � WindowState$ 	Maximized=�.����    styleBorder#��[  ._��  � RowDefinitions �� $��75 ��� $	��630 ��� $��45 �.+��� $��0�� �� Orientation$ 
Horizontal=�$	��Top=�.+��� -    ��Name$ labTitle��$
��1060��  styleLblBold#��[ $��Report Title��&   System.Windows.Controls.DataGrid  $��1��$
��Left=�$��	10,0,10,0q�$
��1328��$	��630��$	��Top=�   AutoGenerateColumns .  ���� *LogCollection+ � 	Resources  �� 
TargetType	 �� Triggers
 P� Property P� Value �� Property �� Value��%�   '��       �� " ��	 P� 
 �@��Orientation$ 
Horizontal=�. ���  � ��$ 30��P� 
 �@��Orientation$ Vertical=�. ���  � ��$ 30��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Column00��-   dgtColumn00��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
Column00+  $ Column01��-   dgtColumn01�� �� *
Column01+  $ Column02��-   dgtColumn02�� �� *
Column02+  $ Column03��-   dgtColumn03�� �� *
Column03+  $ Column04��-   dgtColumn04�� �� *
Column04+  $ Column05��-   dgtColumn05�� �� *
Column05+  $ Column06��-   dgtColumn06�� �� *
Column06+  $ Column07��-	   dgtColumn07�� �� *
Column07+  $ Column08��-
   dgtColumn08�� �� *
Column08+  $ Column09��-   dgtColumn09�� �� *
Column09+  $ Column10��-   dgtColumn10�� �� *
Column10+  $ Column11��-   dgtColumn11�� �� *
Column11+  $ Column12��-   dgtColumn12�� �� *
Column12+  $ Column13��-   dgtColumn13�� �� *
Column13+  $ Column14��-   dgtColumn14�� �� *
Column14+  $ Column15��-   dgtColumn15�� �� *
Column15+  $ Column16��-   dgtColumn16�� �� *
Column16+  $ Column17��-   dgtColumn17�� �� *
Column17+  $ Column18��-   dgtColumn18�� �� *
Column18+  $ Column19��-   dgtColumn19�� �� *
Column19+  $ Column20��-   dgtColumn20�� �� *
Column20+  $ Column21��-   dgtColumn21�� �� *
Column21+  $ Column22��-   dgtColumn22�� �� *
Column22+  $ Column23��-   dgtColumn23�� �� *
Column23+  $ Column24��-   dgtColumn24�� �� *
Column24+  $ Column25��-   dgtColumn25�� �� *
Column25+  $ Column26��-   dgtColumn26�� �� *
Column26+  $ Column27��-   dgtColumn27�� �� *
Column27+  $ Column28��-   dgtColumn28�� �� *
Column28+  $ Column29��-   dgtColumn29�� �� *
Column29+  $ Column30��-    dgtColumn30�� �� *
Column30+  $ Column31��-!   dgtColumn31�� �� *
Column31+  $ Column32��-"   dgtColumn32�� �� *
Column32+  $ Column33��-#   dgtColumn33�� �� *
Column33+  $ Column34��-$   dgtColumn34�� �� *
Column34+� $��2�� � ColumnDefinitions �� $��20 ��� $	��150 ��� $��20 ��� $��* ��� $��20 ��� $	��150 ��� $��20 �.+��� -%    ��Name$ btnSave��$��1��  styleBtnStandard#��[ $��xSave���� -&   $��tbkSaveMessage��$��3��  styleTbkHeader#��[ $��Center=�$��Center=�$��24(��� -'   $ btnClose��$��5��#��[ $��xClose��