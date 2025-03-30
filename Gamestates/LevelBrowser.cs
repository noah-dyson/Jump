using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Newtonsoft.Json;

namespace CS_Coursework;

public class LevelBrowser : Gamestate {
    private ButtonBar _backgroundBar;
    private ButtonBar _navBar;
    private Button _searchButton, _backButton;
    private BrowserPage _browserPage;
    private ButtonBar _pageChanger;
    private Button _nextPage, _previousPage;
    private List<Level> _levels;
    private static int _itemsPerPage = 5;
    private int _page = 0;
    private TextBox _searchBar;
    public bool PendingBrowserReset = false;

    public override void LoadContent() {
        int pad = 10;
        int height = 80;
        int width = Game1.SCREEN_WIDTH - 2 * pad;

        // search bar section
        _backgroundBar = new ButtonBar(new Vector2(pad, pad), width, height);
        _navBar = new ButtonBar(new Vector2(pad + Game1.SCREEN_WIDTH / 2, pad), width / 2, height);
        _searchButton = new Button(120, 30, "Search");
        _backButton = new Button(120, 30, "Back");
        // adds font and creates textbox
        SpriteFont font = Game1.ContentManager.Load<SpriteFont>("MediumFont");
        _searchBar = new TextBox(new Vector2(pad * 2, pad + 2), width / 2, height - 10, font);

        _backButton.Clicked += BackButton_Clicked;
        _searchButton.Clicked += SearchButton_Clicked;

        _navBar.Buttons = new List<Button>() { _searchButton, _backButton
        };
        _navBar.SetButtonPositions();
        _backgroundBar.Buttons = new List<Button>() { };

        AddObject(_backgroundBar);
        AddObject(_navBar);
        AddObject(_searchBar);
        Game1.CurrentWindow.TextInput += Window_TextInput;

        _browserPage = new BrowserPage(new Vector2(200, 200), width - 400 + pad * 2, 100);
        AddObject(_browserPage);

        LoadLevels();
        LoadNewBrowserItems(_page);

        _pageChanger = new ButtonBar(new Vector2(pad, Game1.SCREEN_HEIGHT - height - pad), 100, 50);
        // creates previous and next page buttons
        _previousPage = new Button(30, 30, "<");
        _nextPage = new Button(30, 30, ">");

        _previousPage.Clicked += PreviousPage_Clicked;
        _nextPage.Clicked += NextPage_Clicked;

        _pageChanger.Buttons = new List<Button>() { _previousPage, _nextPage };
        _pageChanger.SetButtonPositions();
        AddObject(_pageChanger);
    }

    public override void Update(GameTime gameTime) {
        // if browser needs to be reset
        if (PendingBrowserReset == true) {
            ResetBrowser();
            PendingBrowserReset = false;
        }
        base.Update(gameTime);
    }

    private void LoadNewBrowserItems(int page) {
        List<BrowserItem> _browserItems = new List<BrowserItem>();
        // by default the number of items on the page is max
        if (_levels.Count != 0) {
            int _numItems = _itemsPerPage;

            // if there are too few items to fit on the page completely
            // then only add the remaining items
            if (_levels.Count % _itemsPerPage != 0 && _page == _levels.Count / _itemsPerPage) {
                _numItems = _levels.Count % _itemsPerPage;
            }

            // loops over corresponding levels based on page number
            foreach (Level level in _levels.GetRange(page * _itemsPerPage, _numItems)) {
                BrowserItem item = new BrowserItem(level, this);
                _browserItems.Add(item);
            }
            _browserPage.BrowserItems = _browserItems;
        }
        else {
            _browserPage.BrowserItems = new List<BrowserItem>();
        }
    }

    private void LoadLevels() {
        // gets list of all files in save file directory
        string path = AppDomain.CurrentDomain.BaseDirectory + "SaveFiles";
        string[] files = Directory.GetFiles(path);
        _levels = new List<Level>();

        // loops across each files and adds data to list of levels
        foreach (string file in files) {
            string levelData = File.ReadAllText(file);
            _levels.Add(JsonConvert.DeserializeObject<Level>(levelData));
        }
    }

    public void ResetBrowser() {
        LoadLevels();
        // checks if that page still exists
        // if it doesn't decrement page
        if (_page >= _levels.Count / _itemsPerPage) {
            _page--;
        }
        LoadNewBrowserItems(_page);
    }

    private void Window_TextInput(object sender, TextInputEventArgs e) {
        if (GamestateManager.CurrentGamestate() == this) {
            _searchBar.HandleTextInput(sender, e);
        }
    }

    private void PreviousPage_Clicked(object sender, EventArgs e) {
        // can only go to previous page if not on first page
        if (_page != 0) {
            _page--;
            LoadNewBrowserItems(_page);
        }
    }

    private void NextPage_Clicked(object sender, EventArgs e) {
        // can only go to next page if not on last page
        if (_page < (_levels.Count - 1) / _itemsPerPage) {
            _page++;
            LoadNewBrowserItems(_page);
        }
    }

    private void BackButton_Clicked(object sender, EventArgs e) {
        GamestateManager.RemoveGamestate();
    }

    private void SearchButton_Clicked(object sender, EventArgs e) {
        // if query not empty
        if (_searchBar.Text != "") {
            List<Level> newLevels = new List<Level>();
            // create new list of levels
            // loop over previous levels, add to list if contain query
            foreach (Level level in _levels) {
                if (level.Name.Contains(_searchBar.Text)) {
                    newLevels.Add(level);
                }
            }
            // set the levels and page to 0
            _levels = newLevels;
            _page = 0;
            LoadNewBrowserItems(_page);
        }
        else {
            // if no search query display everything
            ResetBrowser();
        }
    }
}
