using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace EveryCent.Controls
{
    public class WrapPanel : Layout<View>
    {
        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create<WrapPanel, StackOrientation>(w => w.Orientation, StackOrientation.Vertical,
                propertyChanged: (bindable, oldvalue, newvalue) => ((WrapPanel)bindable).OnSizeChanged());

        public StackOrientation Orientation
        {
            get { return (StackOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public readonly BindableProperty SpacingProperty =
            BindableProperty.Create<WrapPanel, double>(w => w.Spacing, 0,
                propertyChanged: (bindable, oldvalue, newvalue) => ((WrapPanel)bindable).OnSizeChanged());

        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create<WrapPanel, DataTemplate>(w => w.ItemTemplate, null,
                propertyChanged: (bindable, oldvalue, newvalue) => ((WrapPanel)bindable).OnSizeChanged());

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create<WrapPanel, IEnumerable>(w => w.ItemsSource, null,
                propertyChanged: (bindable, oldvalue, newvalue) => ((WrapPanel)bindable).ItemsSource_OnPropertyChanged(bindable, oldvalue, newvalue));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty TemplateSelectorProperty =
            BindableProperty.Create<WrapPanel, DataTemplateSelector>(w => w.TemplateSelector, null,
                propertyChanged: (bindable, oldvalue, newvalue) => ((WrapPanel)bindable).OnSizeChanged());

        public DataTemplateSelector TemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(TemplateSelectorProperty); }
            set { SetValue(TemplateSelectorProperty, value); }
        }

        public static readonly BindableProperty MaxItemsRowProperty = BindableProperty.Create(
            "MaxItemsRow",
            typeof(int),
            typeof(WrapPanel),
            0,
                propertyChanged: (bindable, oldValue, newValue) => { }
            );

        public int MaxItemsRow
        {
            get { return (int)GetValue(MaxItemsRowProperty); }
            set { SetValue(MaxItemsRowProperty, value); }
        }

        public static readonly BindableProperty StartPositionXProperty = BindableProperty.Create(
            "StartPositionX",
            typeof(double),
            typeof(WrapPanel),
            0d,
                propertyChanged: (bindable, oldValue, newValue) => { }
            );

        public double StartPositionX
        {
            get { return (double)GetValue(StartPositionXProperty); }
            set { SetValue(StartPositionXProperty, value); }
        }

        public WrapPanel()
        {

        }

        private void ItemsSource_OnPropertyChanged(BindableObject bindable, IEnumerable oldvalue, IEnumerable newvalue)
        {
            if (oldvalue != null)
            {
                var observableCollection = oldvalue as INotifyCollectionChanged;

                // Unsubscribe from CollectionChanged on the old collection
                if (observableCollection != null)
                    observableCollection.CollectionChanged -= OnCollectionChanged;
            }

            if (newvalue != null)
            {
                var observableCollection = newvalue as INotifyCollectionChanged;

                // Subscribe to CollectionChanged on the new collection 
                //and fire the CollectionChanged event to handle the items in the new collection
                if (observableCollection != null)
                    observableCollection.CollectionChanged += OnCollectionChanged;

                Children.Clear();
                AddItems(newvalue);
            }
        }


        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    Children.Clear();
                    break;

                case NotifyCollectionChangedAction.Add:
                    AddItems(args.NewItems);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    RemoveItems(args.OldItems);
                    break;
            }
        }

        private void RemoveItems(IEnumerable items)
        {
            foreach (object item in items)
            {
                var child = Children.FirstOrDefault(c => c.BindingContext == item);
                if (child != null)
                    Children.Remove(child);
            }
        }

        private void AddItems(IEnumerable items)
        {
            foreach (object item in items)
            {
                var child = CreateViewFor(item);
                if (child == null)
                    return;

                child.BindingContext = item;
                Children.Add(child);
            }
        }

        //TODO Are the following two needed at this point?
        //Are they overrides to support clicking?

        /// <summary>
        /// Creates a View for the type of item passed
        /// </summary>
        /// <param name="item">Item to bind to the view</param>
        /// <returns>The view created</returns>
        protected virtual View CreateViewFor(object item)
        {
            //var template = GetTemplateFor(item.GetType());
            var template = GetTemplateFor(item);
            var content = template.CreateContent();

            if (!(content is View) && !(content is ViewCell))
                throw new Exception(content.GetType().ToString());

            var view = (content is View) ? content as View : ((ViewCell)content).View;
            view.BindingContext = item;
            
            view.GestureRecognizers.Add(new TapGestureRecognizer { Command = ItemClickCommand, CommandParameter = item });
            return view;
        }

        private ICommand _itemClickCommand;
        public ICommand ItemClickCommand
        {
            get
            {
                return _itemClickCommand ?? (_itemClickCommand = new Command((param) =>
                {
                    var _navigationService = Base.LocatorBase.Resolve<Services.INavigationService>();
                    _navigationService.NavigateToAsync<ViewModels.MovementsDayViewModel>((Model.Day)param);
                }));
            }
        }

        /// <summary>
        /// Get's a DataTemplate for the Type passed
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The DataTemplate for the type</returns> 
        protected virtual DataTemplate GetTemplateFor(Type type)
        {
            var template = ItemTemplate;

            //if (TemplateSelector != null)
            //    template = TemplateSelector.TemplateFor(type);            

            return template;
        }

        protected virtual DataTemplate GetTemplateFor(object item)
        {
            var template = ItemTemplate;
            if (TemplateSelector != null)
            {
                template = TemplateSelector.SelectTemplate(item, null);
            }
            return template;
        }

        /// <summary>
        /// Called when the spacing or orientation properties are changed - it forces
        /// the control to go back through a layout pass.
        /// </summary>
        private void OnSizeChanged()
        {
            ForceLayout();
        }

        /// <summary>
        /// Called during the measure pass of a layout cycle to get the desired size of an element.
        /// </summary>
        /// <param name="widthConstraint">The available width for the element to use.</param>
        /// <param name="heightConstraint">The available height for the element to use.</param>
        protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
        {
            if (WidthRequest > 0)
                widthConstraint = Math.Min(widthConstraint, WidthRequest);
            if (HeightRequest > 0)
                heightConstraint = Math.Min(heightConstraint, HeightRequest);

            double internalWidth = double.IsPositiveInfinity(widthConstraint) ? double.PositiveInfinity : Math.Max(0, widthConstraint);
            double internalHeight = double.IsPositiveInfinity(heightConstraint) ? double.PositiveInfinity : Math.Max(0, heightConstraint);

            return Orientation == StackOrientation.Vertical
                ? DoVerticalMeasure(internalWidth, internalHeight)
                    : DoHorizontalMeasure(internalWidth, internalHeight);

        }

        private SizeRequest DoVerticalMeasure(double widthConstraint, double heightConstraint)
        {
            int columnCount = 1;

            double width = 0;
            double height = 0;
            double minWidth = 0;
            double minHeight = 0;
            double heightUsed = 0;

            foreach (var item in Children)
            {
                var size = item.Measure(widthConstraint, heightConstraint);
                width = Math.Max(width, size.Request.Width);

                var newHeight = height + size.Request.Height + Spacing;
                if (newHeight > heightConstraint)
                {
                    columnCount++;
                    heightUsed = Math.Max(height, heightUsed);
                    height = size.Request.Height;
                }
                else
                    height = newHeight;

                minHeight = Math.Max(minHeight, size.Minimum.Height);
                minWidth = Math.Max(minWidth, size.Minimum.Width);
            }

            if (columnCount > 1)
            {
                height = Math.Max(height, heightUsed);
                width *= columnCount;  // take max width
            }

            return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));
        }

        /// <summary>
        /// Does the horizontal measure.
        /// </summary>
        /// <returns>The horizontal measure.</returns>
        /// <param name="widthConstraint">Width constraint.</param>
        /// <param name="heightConstraint">Height constraint.</param>
        private SizeRequest DoHorizontalMeasure(double widthConstraint, double heightConstraint)
        {
            int rowCount = 1;

            double width = 0;
            double height = 0;
            double minWidth = 0;
            double minHeight = 0;
            double widthUsed = 0;

            foreach (var item in Children)
            {
                var size = item.Measure(widthConstraint, heightConstraint);
                height = Math.Max(height, size.Request.Height);

                var newWidth = width + size.Request.Width + Spacing;
                if (newWidth > widthConstraint)
                {
                    rowCount++;
                    widthUsed = Math.Max(width, widthUsed);
                    width = size.Request.Width;
                }
                else
                    width = newWidth;

                minHeight = Math.Max(minHeight, size.Minimum.Height);
                minWidth = Math.Max(minWidth, size.Minimum.Width);
            }

            if (rowCount > 1)
            {
                width = Math.Max(width, widthUsed);
                height = (height + Spacing) * rowCount - Spacing;
            }

            return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));
        }

        /// <summary>
        /// Positions and sizes the children of a Layout.
        /// </summary>
        /// <param name="x">A value representing the x coordinate of the child region bounding box.</param>
        /// <param name="y">A value representing the y coordinate of the child region bounding box.</param>
        /// <param name="width">A value representing the width of the child region bounding box.</param>
        /// <param name="height">A value representing the height of the child region bounding box.</param>
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            if (Orientation == StackOrientation.Vertical)
            {
                double colWidth = 0;
                double yPos = y, xPos = x;

                foreach (var child in Children.Where(c => c.IsVisible))
                {
                    var request = child.Measure(width, height);

                    double childWidth = request.Request.Width;
                    double childHeight = request.Request.Height;
                    colWidth = Math.Max(colWidth, childWidth);

                    if (yPos + childHeight > height)
                    {
                        yPos = y;
                        xPos += colWidth + Spacing;
                        colWidth = 0;
                    }

                    var region = new Rectangle(xPos, yPos, childWidth, childHeight);
                    LayoutChildIntoBoundingRegion(child, region);
                    yPos += region.Height + Spacing;
                }
            }
            else
            {
                double rowHeight = 0;
                double yPos = y, xPos = x;

                int rowChildren = 0;
                bool firstRow = true;
                if (StartPositionX > 0)
                    x = StartPositionX;

                foreach (var child in Children.Where(c => c.IsVisible))
                {
                    var request = child.Measure(width, height);

                    double childWidth = request.Request.Width;
                    double childHeight = request.Request.Height;
                    rowHeight = Math.Max(rowHeight, childHeight);

                    if (MaxItemsRow > 0)
                    {
                        //childWidth = width / MaxItemsRow + 1;
                        //childHeight = childWidth;
                        if (rowChildren % MaxItemsRow == 0)
                        {
                            xPos = x;
                            if (firstRow)
                            {
                                yPos += Spacing;
                                firstRow = false;
                            }
                            else
                                yPos += rowHeight + Spacing;
                            rowHeight = 0;
                        }
                        rowChildren++;
                    }
                    else
                    {
                        if (xPos + childWidth > width)
                        {
                            xPos = x;
                            yPos += rowHeight + Spacing;
                            rowHeight = 0;
                        }
                    }

                    var region = new Rectangle(xPos, yPos, childWidth, childHeight);
                    LayoutChildIntoBoundingRegion(child, region);
                    xPos += region.Width + Spacing;
                }

            }
        }
    }

    public enum WrapOrientation
    {
        HorizontalThenVertical,
        VerticalThenHorizontal
    }

    public class WrapLayout : Layout<Xamarin.Forms.View>
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(WrapLayout),
                null, propertyChanged: OnItemsSourceChanged);

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(WrapLayout));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        static void OnItemsSourceChanged(BindableObject bindable, Object oldValue, Object newValue)
        {
            if (Equals(newValue, null) && Equals(oldValue, null))
            {
                return;
            }
            var wrappanel = (WrapLayout)bindable;
            wrappanel.InstanceOnItemsSourceChanged(oldValue, newValue);
        }

        void InstanceOnItemsSourceChanged(Object oldValue, Object newValue)
        {
            this.Children.Clear();

            var oldCollectionINotifyCollectionChanged = oldValue as INotifyCollectionChanged;
            if (oldCollectionINotifyCollectionChanged != null)
            {
                oldCollectionINotifyCollectionChanged.CollectionChanged -= ItemsSource_CollectionChanged;
            }

            var newCollectionINotifyCollectionChanged = newValue as INotifyCollectionChanged;
            if (newCollectionINotifyCollectionChanged != null)
            {
                newCollectionINotifyCollectionChanged.CollectionChanged += ItemsSource_CollectionChanged;
            }
            if (newValue != null)
            {
                foreach (var item in (IList)newValue)
                {
                    var child = ItemTemplate.CreateContent() as Xamarin.Forms.View;
                    if (child == null)
                        return;
                    child.BindingContext = item;
                    this.Children.Add(child);
                }
            }
        }

        void ItemsSource_CollectionChanged(Object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in e.NewItems)
            {
                var child = ItemTemplate.CreateContent() as Xamarin.Forms.View;
                if (child == null)
                    return;
                child.BindingContext = item;
                this.Children.Add(child);
            }
        }

        public WrapLayout()
        {

        }

        struct LayoutInfo
        {
            public LayoutInfo(int visibleChildCount, Size cellSize, int rows, int cols) : this()
            {
                VisibleChildCount = visibleChildCount;
                CellSize = cellSize;
                Rows = rows;
                Cols = cols;
            }

            public int VisibleChildCount { private set; get; }

            public Size CellSize { private set; get; }

            public int Rows { private set; get; }

            public int Cols { private set; get; }
        }

        Dictionary<Size, LayoutInfo> layoutInfoCache = new Dictionary<Size, LayoutInfo>();

        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create(
                "Orientation",
                typeof(WrapOrientation),
                typeof(WrapLayout),
                WrapOrientation.HorizontalThenVertical,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((WrapLayout)bindable).InvalidateLayout();
                });

        public static readonly BindableProperty ColumnSpacingProperty =
            BindableProperty.Create(
                "ColumnSpacing",
                typeof(double),
                typeof(WrapLayout),
                5.0,
                propertyChanged: (bindable, oldvalue, newvalue) =>
                {
                    ((WrapLayout)bindable).InvalidateLayout();
                });

        public static readonly BindableProperty RowSpacingProperty =
            BindableProperty.Create(
                "RowSpacing",
                typeof(double),
                typeof(WrapLayout),
                5.0,
                propertyChanged: (bindable, oldvalue, newvalue) =>
                {
                    ((WrapLayout)bindable).InvalidateLayout();
                });

        public WrapOrientation Orientation
        {
            set { SetValue(OrientationProperty, value); }
            get { return (WrapOrientation)GetValue(OrientationProperty); }
        }

        public double ColumnSpacing
        {
            set { SetValue(ColumnSpacingProperty, value); }
            get { return (double)GetValue(ColumnSpacingProperty); }
        }

        public double RowSpacing
        {
            set { SetValue(RowSpacingProperty, value); }
            get { return (double)GetValue(RowSpacingProperty); }
        }

        protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
        {
            LayoutInfo layoutInfo = GetLayoutInfo(widthConstraint, heightConstraint);

            if (layoutInfo.VisibleChildCount == 0)
            {
                return new SizeRequest();
            }

            Size totalSize = new Size(layoutInfo.CellSize.Width * layoutInfo.Cols +
                                        ColumnSpacing * (layoutInfo.Cols - 1),
                                      layoutInfo.CellSize.Height * layoutInfo.Rows +
                                        RowSpacing * (layoutInfo.Rows - 1));

            return new SizeRequest(totalSize);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            LayoutInfo layoutInfo = GetLayoutInfo(width, height);

            if (layoutInfo.VisibleChildCount == 0)
                return;

            double xChild = x;
            double yChild = y;
            int row = 0;
            int col = 0;

            foreach (Xamarin.Forms.View child in Children)
            {
                if (!child.IsVisible)
                    continue;

                LayoutChildIntoBoundingRegion(child,
                        new Rectangle(new Point(xChild, yChild), layoutInfo.CellSize));

                if (Orientation == WrapOrientation.HorizontalThenVertical)
                {
                    if (++col == layoutInfo.Cols)
                    {
                        col = 0;
                        row++;
                        xChild = x;
                        yChild += RowSpacing + layoutInfo.CellSize.Height;
                    }
                    else
                    {
                        xChild += ColumnSpacing + layoutInfo.CellSize.Width;
                    }
                }
                else // Orientation == WrapOrientation.VerticalThenHorizontal
                {
                    if (++row == layoutInfo.Rows)
                    {
                        col++;
                        row = 0;
                        xChild += ColumnSpacing + layoutInfo.CellSize.Width;
                        yChild = y;
                    }
                    else
                    {
                        yChild += RowSpacing + layoutInfo.CellSize.Height;
                    }
                }
            }
        }

        LayoutInfo GetLayoutInfo(double width, double height)
        {
            Size size = new Size(width, height);

            // Check if cached information is available.
            if (layoutInfoCache.ContainsKey(size))
            {
                return layoutInfoCache[size];
            }

            int visibleChildCount = 0;
            Size maxChildSize = new Size();
            int rows = 0;
            int cols = 0;
            LayoutInfo layoutInfo = new LayoutInfo();

            // Enumerate through all the children.
            foreach (Xamarin.Forms.View child in Children)
            {
                // Skip invisible children.
                if (!child.IsVisible)
                    continue;

                // Count the visible children.
                visibleChildCount++;

                // Get the child's requested size.
                //SizeRequest childSizeRequest = child.GetSizeRequest(Double.PositiveInfinity,
                //                                                    Double.PositiveInfinity);
                SizeRequest childSizeRequest = child.Measure(Double.PositiveInfinity, Double.PositiveInfinity);

                // Accumulate the maximum child size.
                maxChildSize.Width =
                    Math.Max(maxChildSize.Width, childSizeRequest.Request.Width);

                maxChildSize.Height =
                    Math.Max(maxChildSize.Height, childSizeRequest.Request.Height);
            }

            if (visibleChildCount != 0)
            {
                // Calculate the number of rows and columns.
                if (Orientation == WrapOrientation.HorizontalThenVertical)
                {
                    if (Double.IsPositiveInfinity(width))
                    {
                        cols = visibleChildCount;
                        rows = 1;
                    }
                    else
                    {
                        cols = (int)((width + ColumnSpacing) /
                                    (maxChildSize.Width + ColumnSpacing));
                        cols = Math.Max(1, cols);
                        rows = (visibleChildCount + cols - 1) / cols;
                    }
                }
                else // WrapOrientation.VerticalThenHorizontal
                {
                    if (Double.IsPositiveInfinity(height))
                    {
                        rows = visibleChildCount;
                        cols = 1;
                    }
                    else
                    {
                        rows = (int)((height + RowSpacing) /
                                    (maxChildSize.Height + RowSpacing));
                        rows = Math.Max(1, rows);
                        cols = (visibleChildCount + rows - 1) / rows;
                    }
                }

                // Now maximize the cell size based on the layout size.
                Size cellSize = new Size();

                if (Double.IsPositiveInfinity(width))
                {
                    cellSize.Width = maxChildSize.Width;
                }
                else
                {
                    cellSize.Width = (width - ColumnSpacing * (cols - 1)) / cols;
                }

                if (Double.IsPositiveInfinity(height))
                {
                    cellSize.Height = maxChildSize.Height;
                }
                else
                {
                    cellSize.Height = (height - RowSpacing * (rows - 1)) / rows;
                }

                layoutInfo = new LayoutInfo(visibleChildCount, cellSize, rows, cols);
            }

            layoutInfoCache.Add(size, layoutInfo);
            return layoutInfo;
        }

        protected override void InvalidateLayout()
        {
            base.InvalidateLayout();

            // Discard all layout information for children added or removed.
            layoutInfoCache.Clear();
        }

        protected override void OnChildMeasureInvalidated()
        {
            base.OnChildMeasureInvalidated();

            // Discard all layout information for child size changed.
            layoutInfoCache.Clear();
        }
    }
}
