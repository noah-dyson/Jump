using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CS_Coursework;

public class LevelEditor : Gamestate
{
    private ButtonBar _navBar;
    private Button _testLevelButton;
    private Button _publishLevelButton;
    private Button _backButton;

    public override void LoadContent()
    {
        _navBar = new ButtonBar(new Vector2(10, 10), 620, 40);
        _testLevelButton = new Button(120, 30, "Test");
        _publishLevelButton = new Button(120, 30, "Publish");
        _backButton = new Button(120, 30, "Back");

        _testLevelButton.Clicked += _testLevelButton_Clicked;
        _publishLevelButton.Clicked += _publishLevelButton_Clicked;
        _backButton.Clicked += _backButton_Clicked;

        _navBar.Buttons = new List<Button>() {_testLevelButton, _publishLevelButton, _backButton};
        _navBar.SetButtonPositions();

        AddObject(_navBar);
    }

    private void _backButton_Clicked(object sender, EventArgs e)
    {
        GamestateManager.RemoveGamestate();
    }

    private void _testLevelButton_Clicked(object sender, EventArgs e)
    {
        Debug.WriteLine("Level loaded for testing");
    }

    private void _publishLevelButton_Clicked(object sender, EventArgs e)
    {
        Debug.WriteLine("Publish level button pressed");
    }
}