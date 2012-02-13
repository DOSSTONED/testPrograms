using App1.Data;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “拆分页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234234 上提供

namespace App1
{
    /// <summary>
    /// 显示组标题、组内各项的列表以及当前选定项的
    /// 详细信息的页。
    /// </summary>
    public sealed partial class SplitPage : App1.Common.LayoutAwarePage
    {
        public SplitPage()
        {
            this.InitializeComponent();
        }

        #region Page state management

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var group = SampleDataSource.GetGroup((String)navigationParameter);
            this.DefaultViewModel["Group"] = group;
            this.DefaultViewModel["Items"] = group.Items;

            if (pageState == null)
            {
                // When this is a new page, select the first item automatically unless logical page
                // navigation is being used (see the logical page navigation #region below.)
                if (!this.UsingLogicalPageNavigation() && this.itemsViewSource.View != null)
                {
                    this.itemsViewSource.View.MoveCurrentToFirst();
                }
            }
            else
            {
                // Restore the previously saved state associated with this page
                if (pageState.ContainsKey("SelectedItem") && this.itemsViewSource.View != null)
                {
                    var selectedItem = SampleDataSource.GetItem((String)pageState["SelectedItem"]);
                    this.itemsViewSource.View.MoveCurrentTo(selectedItem);
                }
            }
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            if (this.itemsViewSource.View != null)
            {
                var selectedItem = (SampleDataItem)this.itemsViewSource.View.CurrentItem;
                if (selectedItem != null) pageState["SelectedItem"] = selectedItem.UniqueId;
            }
        }

        #endregion

        #region 逻辑页导航

        // 视觉状态管理通常直接反映四种应用程序视图状态
        // (全屏横向与纵向以及对齐与填充视图。)设计拆分页
        // 的目的在于使对齐和纵向视图状态均有两个不同的子状态:
        // 显示项列表或详细信息之一，但不同时显示。
        //
        // 这完全通过一个可表示两个逻辑页的单一物理页
        // 实现。使用下面的代码可以实现此目标，且用户不会察觉到
        // 区别。

        /// <summary>
        /// 在确定该页是应用作一个逻辑页还是两个逻辑页时进行调用。
        /// </summary>
        /// <param name="viewState">提出的问题所针对的视图状态，或者
        /// 为 null (对于当前视图状态)。此参数是可选的，默认值为
        /// null。</param>
        /// <returns>当相关视图状态为纵向或对齐时为 true，否则为 false
        /// 。</returns>
        private bool UsingLogicalPageNavigation(ApplicationViewState? viewState = null)
        {
            if (viewState == null) viewState = ApplicationView.Value;
            return viewState == ApplicationViewState.FullScreenPortrait ||
                viewState == ApplicationViewState.Snapped;
        }

        /// <summary>
        /// 在选定列表中的项时进行调用。
        /// </summary>
        /// <param name="sender">显示所单击项的 GridView (在应用程序处于对齐状态时
        /// 的 ListView)。</param>
        /// <param name="e">描述如何更改选择内容的事件数据。</param>
        void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 使视图状态在逻辑页导航起作用时无效，因为
            // 选择内容方面的更改可能会导致当前逻辑页发生相应的更改。
            // 选定某项后，这将会导致从显示项列表
            // 更改为显示选定项的详细信息。清除选择时，将产生
            // 相反的效果。
            if (this.UsingLogicalPageNavigation()) this.InvalidateVisualState();
        }

        /// <summary>
        /// 在按页上的后退按钮时进行调用。
        /// </summary>
        /// <param name="sender">后退按钮实例。</param>
        /// <param name="e">描述如何单击后退按钮的事件数据。</param>
        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            if (this.UsingLogicalPageNavigation() && itemListView.SelectedItem != null)
            {
                // 如果逻辑页导航起作用且存在选定项，则当前将显示
                // 选定项的详细信息。清除选择后将返回到
                // 项列表。从用户的角度来看，这是一个逻辑后向
                // 导航。
                this.itemListView.SelectedItem = null;
            }
            else
            {
                // 如果逻辑页导航不起作用或者不存在任何选定项，
                // 则使用默认的后退按钮行为。
                base.GoBack(sender, e);
            }
        }

        /// <summary>
        /// 在确定对应于应用程序视图状态的视觉状态的名称时进行
        /// 视图状态。
        /// </summary>
        /// <param name="viewState">提出的问题所针对的视图状态。</param>
        /// <returns>所需的视觉状态的名称。此名称与视图状态的名称相同，
        /// 但在纵向和对齐视图中存在选定项时例外，在纵向和对齐视图中，
        /// 此附加逻辑页通过添加 _Detail 后缀表示。</returns>
        protected override string DetermineVisualState(ApplicationViewState viewState)
        {
            // 在视图状态更改时更新后退按钮的启用状态
            var logicalPageBack = this.UsingLogicalPageNavigation(viewState) && this.itemListView.SelectedItem != null;
            var physicalPageBack = this.Frame != null && this.Frame.CanGoBack;
            this.DefaultViewModel["CanGoBack"] = logicalPageBack || physicalPageBack;

            // 基于窗口的宽度(而非视图状态)来确定横向布局的
            // 可视状态。此页面具有一个适用于
            // 1366 个虚拟像素或更宽的显示屏的布局，还具有一个适用于较窄的显示屏(或对齐的
            // 应用程序将可用水平空间减小为小于 1366 个像素的情况)的布局。
            if (viewState == ApplicationViewState.Filled ||
                viewState == ApplicationViewState.FullScreenLandscape)
            {
                var windowWidth = Window.Current.Bounds.Width;
                if (windowWidth >= 1366) return "FullScreenLandscapeOrWide";
                return "FilledOrNarrow";
            }

            // 在纵向或对齐视图中最开始显示默认可视状态名称，然后在查看详细信息(而不是列表)
            // 时添加后缀
            var defaultStateName = base.DetermineVisualState(viewState);
            return logicalPageBack ? defaultStateName + "_Detail" : defaultStateName;
        }

        #endregion
    }
}
