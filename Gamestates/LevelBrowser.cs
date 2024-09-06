using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CS_Coursework;

public class LevelBrowser : Gamestate
{
    private ButtonBar _navBar;
    private Button _localLevelsButton;
    private Button _communityLevelsButton;
    private Button _backButton;
    private ButtonBar _sortBar;
    private Button _sortLikesButton;
    private Button _sortPlaysButton;
    private Button _sortTrendingButton;

    public override void LoadContent()
    {
        _navBar = new ButtonBar(new Vector2(10, 10), 620, 40);
        _localLevelsButton = new Button(120, 30, "Local");
        _communityLevelsButton = new Button(120, 30, "Community");
        _backButton = new Button(120, 30, "Back");

        _localLevelsButton.Clicked += LocalLevelsButton_Clicked;
        _communityLevelsButton.Clicked += CommunityLevelButton_Clicked;
        _backButton.Clicked += BackButton_Clicked;

        _navBar.Buttons = new List<Button>() {_localLevelsButton, _communityLevelsButton, _backButton};
        _navBar.SetButtonPositions();

        AddObject(_navBar);

        _sortBar = new ButtonBar(new Vector2(10, 60), 620, 40);
        _sortLikesButton = new Button(120, 30, "Likes");
        _sortPlaysButton = new Button(120, 30, "Plays");
        _sortTrendingButton = new Button(120, 30, "Trending");

        _sortLikesButton.Clicked += SortLikesButton_Clicked;
        _sortPlaysButton.Clicked += SortPlaysButton_Clicked;
        _sortTrendingButton.Clicked += SortTrendingButton_Clicked;

        _sortBar.Buttons = new List<Button>() {_sortLikesButton, _sortPlaysButton, _sortTrendingButton};
        _sortBar.SetButtonPositions();

        AddObject(_sortBar);
    }

    private void SortLikesButton_Clicked(object sender, EventArgs e)
    {
        Debug.WriteLine("Sorting by Likes");
    }

    private void SortPlaysButton_Clicked(object sender, EventArgs e)
    {
        Debug.WriteLine("Sorting by Plays");
    }

    private void SortTrendingButton_Clicked(object sender, EventArgs e)
    {
        Debug.WriteLine("Sorting by Trending");
    }

    private void LocalLevelsButton_Clicked(object sender, EventArgs e)
    {
        Debug.WriteLine("Local levels loaded");
    }

    private void CommunityLevelButton_Clicked(object sender, EventArgs e)
    {
        Debug.WriteLine("Community levels loaded");
    }

    private void BackButton_Clicked(object sender, EventArgs e)
    {
        GamestateManager.RemoveGamestate();
    }
}