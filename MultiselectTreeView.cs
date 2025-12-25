using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace DoomLoader
{
    public sealed class MultiselectTreeview : TreeView
    {
        private TreeViewItem _lastItemSelected;
        public static readonly DependencyProperty IsItemSelectedProperty = DependencyProperty.RegisterAttached("IsItemSelected", typeof(bool), typeof(MultiselectTreeview));

        public static void SetIsItemSelected(UIElement element, bool value) => element.SetValue(IsItemSelectedProperty, (object)value);

        public static bool GetIsItemSelected(UIElement element) => (bool)element.GetValue(IsItemSelectedProperty);

        private static bool IsCtrlPressed => Keyboard.IsKeyDown((Key)118) || Keyboard.IsKeyDown((Key)119);

        private static bool IsShiftPressed => Keyboard.IsKeyDown((Key)116) || Keyboard.IsKeyDown((Key)117);

        public IList SelectedItems => GetTreeViewItems(this, true).Where(new Func<TreeViewItem, bool>(GetIsItemSelected)).Select(treeViewItem => treeViewItem.Header).ToList();

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            if (e.OriginalSource is Shape || e.OriginalSource is Grid || e.OriginalSource is Border)
                return;
            TreeViewItem treeViewItemClicked = GetTreeViewItemClicked((DependencyObject)e.OriginalSource);
            if (treeViewItemClicked == null)
                return;
            SelectedItemChangedInternal(treeViewItemClicked);
        }

        private void SelectedItemChangedInternal(TreeViewItem tvItem)
        {
            if (!IsCtrlPressed)
            {
                foreach (UIElement treeViewItem in GetTreeViewItems(this, true))
                    SetIsItemSelected(treeViewItem, false);
            }
            if (IsShiftPressed && _lastItemSelected != null)
            {
                List<TreeViewItem> treeViewItemRange = GetTreeViewItemRange(_lastItemSelected, tvItem);
                if (treeViewItemRange.Count <= 0)
                    return;
                foreach (UIElement element in treeViewItemRange)
                    SetIsItemSelected(element, true);
                _lastItemSelected = treeViewItemRange.Last();
            }
            else
            {
                SetIsItemSelected(tvItem, true);
                _lastItemSelected = tvItem;
            }
        }

        private static TreeViewItem GetTreeViewItemClicked(DependencyObject sender)
        {
            while (true)
            {
                switch (sender)
                {
                    case null:
                    case TreeViewItem _:
                        goto label_3;
                    default:
                        sender = VisualTreeHelper.GetParent(sender);
                        continue;
                }
            }
        label_3:
            return sender as TreeViewItem;
        }

        private static List<TreeViewItem> GetTreeViewItems(
          ItemsControl parentItem,
          bool includeCollapsedItems,
          List<TreeViewItem> itemList = null)
        {
            if (itemList == null)
                itemList = new List<TreeViewItem>();
            for (int index = 0; index < parentItem.Items.Count; ++index)
            {
                if (parentItem.ItemContainerGenerator.ContainerFromIndex(index) is TreeViewItem parentItem1)
                {
                    itemList.Add(parentItem1);
                    if (includeCollapsedItems || parentItem1.IsExpanded)
                        GetTreeViewItems(parentItem1, includeCollapsedItems, itemList);
                }
            }
            return itemList;
        }

        private List<TreeViewItem> GetTreeViewItemRange(TreeViewItem start, TreeViewItem end)
        {
            List<TreeViewItem> treeViewItems = GetTreeViewItems(this, false);
            int num1 = treeViewItems.IndexOf(start);
            int num2 = treeViewItems.IndexOf(end);
            int index = num1 > num2 || num1 == -1 ? num2 : num1;
            int count = num1 > num2 ? num1 - num2 + 1 : num2 - num1 + 1;
            if (num1 == -1 && num2 == -1)
                count = 0;
            else if (num1 == -1 || num2 == -1)
                count = 1;
            return count <= 0 ? new List<TreeViewItem>() : treeViewItems.GetRange(index, count);
        }
    }
}
