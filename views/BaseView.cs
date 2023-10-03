using System.Windows;
using System.Windows.Controls;

namespace LiftSystem.views
{
    public class BaseView : Window
    {
        private readonly int _fullWidth = (int)SystemParameters.WorkArea.Width;
        private readonly int _fullHeight = (int)SystemParameters.WorkArea.Height;
        
        private readonly int _leftPanelWidth = (int)(SystemParameters.WorkArea.Width * Constants.LeftPanelWidthPercent);
        private readonly int _rightPanelWidth =
            (int)(SystemParameters.WorkArea.Height * Constants.RightPanelWidthPercent);

        private StackPanel _leftPanel;
        private StackPanel _rightPanel;
        
        public BaseView()
        {
            WindowState = WindowState.Maximized;
            ResizeMode = ResizeMode.NoResize;
            Width = _fullWidth;
            Height = _fullHeight;
            
            Title = "Lift System";
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            var basePanel = new Grid();
            basePanel.Width = _fullWidth;
            basePanel.Height = _fullHeight;

            var leftColumn = new ColumnDefinition();
            leftColumn.Width = new GridLength(_leftPanelWidth, GridUnitType.Pixel);
            basePanel.ColumnDefinitions.Add(leftColumn);

            var rightColumn = new ColumnDefinition();
            leftColumn.Width = new GridLength(_rightPanelWidth, GridUnitType.Pixel);
            basePanel.ColumnDefinitions.Add(rightColumn);

            _leftPanel = new StackPanel();
            Grid.SetColumn(_leftPanel, 0);
            basePanel.Children.Add(_leftPanel);

            _rightPanel = new StackPanel();
            Grid.SetColumn(_rightPanel, 1);
            basePanel.Children.Add(_rightPanel);

            Content = basePanel;
        }

        public int PanelHeight => _fullHeight;
        public int LeftPanelWidth => _leftPanelWidth;
        public int RightPanelWidth => _rightPanelWidth;

        public StackPanel LeftPanel => _leftPanel;
        public StackPanel RightPanel => _rightPanel;
    }
}