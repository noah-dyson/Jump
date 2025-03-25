using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.Xna.Framework;

namespace CS_Coursework;

public class LevelBrowser : Gamestate {
    private ButtonBar _navBar;
    private Button _localLevelsButton;
    private Button _communityLevelsButton;
    private Button _backButton;
    private ButtonBar _sortBar;
    private Button _sortLikesButton;
    private Button _sortPlaysButton;
    private Button _sortTrendingButton;
    private BrowserPage _browserPage;
    private ButtonBar _pageChanger;
    private Button _nextPage, _previousPage;
    private List<string> _levels = new List<string>() { "1", "2", "3", "4", "5" };
    private static int _itemsPerPage = 2;
    private int _page = 0;

    public override void LoadContent() {
        int pad = 10;
        int height = 40;
        int width = Game1.SCREEN_WIDTH - 2 * pad;

        _navBar = new ButtonBar(new Vector2(pad, pad), width, height);
        _localLevelsButton = new Button(120, 30, "Local");
        _communityLevelsButton = new Button(120, 30, "Community");
        _backButton = new Button(120, 30, "Back");

        _localLevelsButton.Clicked += LocalLevelsButton_Clicked;
        _communityLevelsButton.Clicked += CommunityLevelButton_Clicked;
        _backButton.Clicked += BackButton_Clicked;

        _navBar.Buttons = new List<Button>() {
            _localLevelsButton, _communityLevelsButton, _backButton
        };
        _navBar.SetButtonPositions();

        AddObject(_navBar);

        _sortBar = new ButtonBar(new Vector2(pad, pad * 2 + height), width, height);
        _sortLikesButton = new Button(120, 30, "Likes");
        _sortPlaysButton = new Button(120, 30, "Plays");
        _sortTrendingButton = new Button(120, 30, "Trending");

        _sortLikesButton.Clicked += SortLikesButton_Clicked;
        _sortPlaysButton.Clicked += SortPlaysButton_Clicked;
        _sortTrendingButton.Clicked += SortTrendingButton_Clicked;

        _sortBar.Buttons = new List<Button>() {
            _sortLikesButton, _sortPlaysButton, _sortTrendingButton
        };
        _sortBar.SetButtonPositions();

        AddObject(_sortBar);

        _browserPage = new BrowserPage(new Vector2(200, 200), width - 400 + pad * 2, 100);
        AddObject(_browserPage);
        LoadNewBrowserItems(_page);

        _pageChanger = new ButtonBar(new Vector2(pad, Game1.SCREEN_HEIGHT - height - pad), 100, height);
        _previousPage = new Button(30, 30, "<");
        _nextPage = new Button(30, 30, ">");
        _previousPage.Clicked += PreviousPage_Clicked;
        _nextPage.Clicked += NextPage_Clicked;
        _pageChanger.Buttons = new List<Button>() { _previousPage, _nextPage };
        _pageChanger.SetButtonPositions();
        AddObject(_pageChanger);
    }

    private void LoadNewBrowserItems(int page) {
        List<BrowserItem> _browserItems = new List<BrowserItem>();
        int _numItems = _itemsPerPage;
        if (_levels.Count % _itemsPerPage != 0 && _page == _levels.Count / _itemsPerPage) {
            _numItems = _levels.Count % _itemsPerPage;
        }
        foreach (string name in _levels.GetRange(page * _itemsPerPage, _numItems)) {
            BrowserItem item = new BrowserItem(name);
            _browserItems.Add(item);
        }
        _browserPage.BrowserItems = _browserItems;
    }

    private void PreviousPage_Clicked(object sender, EventArgs e) {
        if (_page != 0) {
            _page--;
            LoadNewBrowserItems(_page);
        }
    }

    private void NextPage_Clicked(object sender, EventArgs e) {
        if (_page < _levels.Count / _itemsPerPage) {
            _page++;
            LoadNewBrowserItems(_page);
        }
    }

    private void SortLikesButton_Clicked(object sender, EventArgs e) {
        Debug.WriteLine("Sorting by Likes");
    }

    private void SortPlaysButton_Clicked(object sender, EventArgs e) {
        Debug.WriteLine("Sorting by Plays");
    }

    private void SortTrendingButton_Clicked(object sender, EventArgs e) {
        Debug.WriteLine("Sorting by Trending");
    }

    private void LocalLevelsButton_Clicked(object sender, EventArgs e) {
        Debug.WriteLine("Local levels loaded");
    }

    private void CommunityLevelButton_Clicked(object sender, EventArgs e) {
        Debug.WriteLine("Community levels loaded");
    }

    private void BackButton_Clicked(object sender, EventArgs e) {
        GamestateManager.RemoveGamestate();
    }
}
