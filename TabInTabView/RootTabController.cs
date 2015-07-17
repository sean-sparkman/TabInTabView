using System;
using UIKit;
using System.Collections.Generic;
using CoreGraphics;

namespace TabInTabView
{
	public class RootTabController
		: UITabBarController
	{
		public RootTabController ()
		{
			ViewControllers = new UIViewController[] {
				CreateController (UIColor.Red, "Red"),
				CreateController (UIColor.Green, "Green"),
				CreateTabController (),
				CreateController (UIColor.Yellow, "Yellow")
			};

			SelectedIndex = 0;
		}

		private UIViewController CreateController (UIColor color, string name)
		{
			var controller = new UIViewController ();
			controller.View.BackgroundColor = color;
			controller.TabBarItem.Title = name;

			return controller;
		}

		private UITabBarController CreateTabController ()
		{
			var tabController = new UITabBarController ();
			tabController.ViewControllers = new UIViewController[] {
				CreateController (UIColor.Purple, "Purple"),
				CreateController (UIColor.Blue, "Blue"),
				CreateController (UIColor.Brown, "Brown")
			};

			foreach (var viewController in tabController.ViewControllers) {
				AddTabNavigationButtons (viewController, tabController.ViewControllers);
			}

			tabController.TabBarItem.Title = "Tabs";
			return tabController;
		}

		private void AddTabNavigationButtons (UIViewController viewController, IEnumerable<UIViewController> destinations)
		{
			int y = 30;
			foreach (var destination in destinations) {
				if (destination != viewController) {
					var button = new UIButton (new CGRect (10, y, 100, 30));
					button.SetTitle (destination.TabBarItem.Title, UIControlState.Normal);
					button.BackgroundColor = destination.View.BackgroundColor;
					button.TouchUpInside += (sender, e) => {
						destination.TabBarController.SelectedViewController = destination;
					};
					viewController.View.AddSubview (button);
					y += 30;
				}
			}
		}
	}
}

