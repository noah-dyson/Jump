using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;

namespace CS_Coursework;

public class LevelEditor : Gamestate {
    private ButtonBar _navBar;
    private Button _testLevelButton, _publishLevelButton, _saveLevelButton, _loadLevelButton, _backButton;
    private ObjectSelector _objectSelector = new ObjectSelector();
    private LevelObjectManager _levelObjectManager = new LevelObjectManager();
    private Grid _grid = new Grid();

    public override void LoadContent() {
        int pad = 32;
        int height = 50;
        int width = Game1.SCREEN_WIDTH - 2 * pad;

        // setup and positioning of buttons
        _navBar = new ButtonBar(new Vector2(pad, pad), width, height);
        _testLevelButton = new Button(120, 40, "Test");
        _publishLevelButton = new Button(120, 40, "Publish");
        _saveLevelButton = new Button(120, 40, "Save");
        _loadLevelButton = new Button(120, 40, "Load");
        _backButton = new Button(120, 40, "Back");

        // applying events to buttons
        _testLevelButton.Clicked += _testLevelButton_Clicked;
        _publishLevelButton.Clicked += _publishLevelButton_Clicked;
        _saveLevelButton.Clicked += _saveLevelButton_Clicked;
        _loadLevelButton.Clicked += _loadLevelButton_Clicked;
        _backButton.Clicked += _backButton_Clicked;

        // adding buttons to button bar
        _navBar.Buttons = new List<Button>() { _testLevelButton, _publishLevelButton, _saveLevelButton, _loadLevelButton, _backButton };
        _navBar.SetButtonPositions();

        AddObject(_navBar);
        _objectSelector.InitializeSelector();
        AddObject(_objectSelector.SelectorBar);
        AddObject(_objectSelector.SelectorBar2);

        ButtonBar test = new ButtonBar(new Vector2(32, 90), 1536, 640);
        test.Buttons = new List<Button>();
        AddObject(test);

        AddObject(_grid.Highlighter);

        _levelObjectManager.CreateCellLabels(this);
    }

    public override void Update(GameTime gameTime) {
        _grid.UpdateGrid(_levelObjectManager, _objectSelector.CurrentObject);
        base.Update(gameTime);
    }

    private void _backButton_Clicked(object sender, EventArgs e) {
        GamestateManager.RemoveGamestate();
    }

    private void _testLevelButton_Clicked(object sender, EventArgs e) {
        Debug.WriteLine("Level loaded for testing");
    }

    private void _publishLevelButton_Clicked(object sender, EventArgs e) {
        Debug.WriteLine("Publish level button pressed");
    }

    private void _saveLevelButton_Clicked(object sender, EventArgs e) {
        // converts the objects 2D array into JSON
        string fileJSON = JsonConvert.SerializeObject(_levelObjectManager.LevelObjects);

        // Saves file to project folder for testing purposes will be changed on release
        string filePath = AppDomain.CurrentDomain.BaseDirectory + "SaveFiles\\test.json";
        File.WriteAllText(filePath, fileJSON);
    }

    private void _loadLevelButton_Clicked(object sender, EventArgs e) {
        // loads the json from the file
        string filePath = AppDomain.CurrentDomain.BaseDirectory + "SaveFiles\\test.json";
        string levelJson = File.ReadAllText(filePath);

        // sets the cell-labels and the level objects from the json
        _levelObjectManager.LoadNewLevelObjects(JsonConvert.DeserializeObject<int[,]>(levelJson));
    }
}
