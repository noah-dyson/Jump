using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace CS_Coursework;

public class LevelEditor : Gamestate
{
    private ButtonBar _navBar;
    private Button _testLevelButton;
    private Button _publishLevelButton;
    private Button _backButton;

    public override void LoadContent()
    {
        int pad = 20;
        int height = 50;
        int width = Game1.SCREEN_WIDTH - 2 * pad;

        // setup and positioning of buttons
        _navBar = new ButtonBar(new Vector2(pad, pad), width, height);
        _testLevelButton = new Button(120, 40, "Test");
        _publishLevelButton = new Button(120, 40, "Publish");
        _backButton = new Button(120, 40, "Back");

        // applying events to buttons
        _testLevelButton.Clicked += _testLevelButton_Clicked;
        _publishLevelButton.Clicked += _publishLevelButton_Clicked;
        _backButton.Clicked += _backButton_Clicked;

        // adding buttons to button bar
        _navBar.Buttons = new List<Button>() {
            _testLevelButton, _publishLevelButton, _backButton
        };
        _navBar.SetButtonPositions();

        AddObject(_navBar);
        ObjectSelector.InitializeSelector();
        AddObject(ObjectSelector.SelectorBar);
        AddObject(ObjectSelector.SelectorBar2);

        ButtonBar test = new ButtonBar(new Vector2(20, 90), 1240, 640);
        test.Buttons = new List<Button>();
        AddObject(test);

        AddObject(Grid.Highlighter);
    }

    public override void Update(GameTime gameTime)
    {
        Grid.UpdateGrid();
        base.Update(gameTime);
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
