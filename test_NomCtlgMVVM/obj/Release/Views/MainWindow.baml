   M S B A M L   `   `   `  ����   NomenclatureCatalogClient'    !test_NomCtlgMVVM.Views.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     0ignorehttp://www.galasoft.ch/ignore     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$-  'Каталог номенклатуры��$	��800��$	��600�� � DataContext �� *Main+   Locator �� Source# [   � 	Resources  �� MergedDictionaries �� Source��  �� $ ../Skins/MainSkin.xaml?�%    .���  � ColumnDefinitions �� $��* ��� $
��auto ��� $��3* � � RowDefinitions �� $
��auto ��� $��* �.+��� $��0��$��0��.+��� .��Поиск по:�� �� .��Опции поиска:�� �� .��	Ищем:��  �� 
IsEditable . R� $��1��$��0��$
��Left=����� *	Folders+�� $��2��$��1��$��3��$��Center=�$��Stretch=�� $��2��$��2�� �� $
��auto ��� $��* ��� $
��auto ��� $��* ��� $
��auto �.+�c� $��0��&   System.Windows.Controls.DataGrid  $��1��	   AutoGenerateColumns	 .  ���� *Nomenclatures+
 �� SelectedItem
 �� *SelectedNomenclature+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    Header$ 
Код 1С��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *1CCode+  $ 5* $ Номенклатура�� �� *Name+4  .System.Windows.Controls.DataGridComboBoxColumn  $ * $ Вид жилы��4  .System.Windows.Controls.DataGridCheckBoxColumn  $ * $ 
Архив�� �� *	IsArchive+  $ * $ Выгрузить в 1С�� �� *
IsSendTo1C+  $ * $ Комментарий�� �� *IsHasComment+4  .System.Windows.Controls.DataGridTemplateColumn  $ * $ Статус���� $��2��$��3��$��Stretch=�$��Center=�  $��3��	 .  ���� *Characteristics+
 �� *SelectedCharacteristic+   $ * $ 
Код 1С�� �� *1CCode+  $ 5* $" Характеристика�� �� *Name+  $ * $ Вид жилы��  $ * $ Цвет�� �� *Color+  $ * $7 1Нестандартная конструкция�� �� *
IsCustom+  $ * $ 
Архив�� �� *	IsArchive+  $ * $ Выгрузить в 1С�� �� *
IsSendTo1C+  $ * $ Комментарий�� �� *IsHasComment+  $ * $ Статус��c� $��4��.@��� $��Новая ном-ра������ *AddNomenclatureCommand+�� $��Новая хар-ка������ *AddCharacteristicCommand+