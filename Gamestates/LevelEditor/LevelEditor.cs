using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;

namespace CS_Coursework;

public class LevelEditor : Gamestate {
    private ButtonBar _navBar;
    private Button _testLevelButton, _publishLevelButton, _saveLevelButton, _saveAsButton, _backButton;
    private ObjectSelector _objectSelector = new ObjectSelector();
    private LevelObjectManager _levelObjectManager = new LevelObjectManager();
    private Grid _grid = new Grid();
    private Level _level;

    public LevelEditor(Level level) {
        _level = level;
    }

    public override void LoadContent() {
        int pad = 32;
        int height = 50;
        int width = Game1.SCREEN_WIDTH - 2 * pad;

        // setup and positioning of buttons
        _navBar = new ButtonBar(new Vector2(pad, pad), width, height);
        _testLevelButton = new Button(120, 40, "Test");
        _publishLevelButton = new Button(120, 40, "Publish");
        _saveLevelButton = new Button(120, 40, "Save");
        _saveAsButton = new Button(120, 40, "Save As");
        _backButton = new Button(120, 40, "Back");

        // applying events to buttons
        _testLevelButton.Clicked += _testLevelButton_Clicked;
        _saveLevelButton.Clicked += _saveLevelButton_Clicked;
        _saveAsButton.Clicked += _saveAsButton_Clicked;
        _backButton.Clicked += _backButton_Clicked;

        // adding buttons to button bar
        _navBar.Buttons = new List<Button>() { _testLevelButton, _saveLevelButton, _saveAsButton, _backButton };
        _navBar.SetButtonPositions();

        AddObject(_navBar);
        _objectSelector.InitializeSelector();
        AddObject(_objectSelector.SelectorBar);
        AddObject(_objectSelector.SelectorBar2);

        ButtonBar test = new ButtonBar(new Vector2(32, 90), 1536, 640);
        test.Buttons = new List<Button>();
        AddObject(test);

        AddObject(_grid.Highlighter);

        _levelObjectManager.CreateLevelObjects(this);
        _levelObjectManager.LoadNewLevelObjects(_level.Ids);
    }

    public override void Update(GameTime gameTime) {
        _grid.UpdateGrid(_levelObjectManager, _objectSelector.CurrentObject);
        base.Update(gameTime);
    }

    private void _backButton_Clicked(object sender, EventArgs e) {
        GamestateManager.RemoveGamestate();
    }

    private void _testLevelButton_Clicked(object sender, EventArgs e) {
        LevelGameplay _testing = new LevelGameplay(new Level("", TimeSpan.Zero, GetIds(_levelObjectManager.EditorObjects)));
        _testing.LoadNewLevelData(GetIds(_levelObjectManager.EditorObjects), true);
        GamestateManager.AddGamestate(_testing);
    }

    private void _saveLevelButton_Clicked(object sender, EventArgs e) {
        // checks if level has a name
        // if it doesn't have a name it has not been loaded from browser
        if (_level.Name != "") {
            string appdataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Jump");
            if (!Directory.Exists(appdataPath)) {
                Directory.CreateDirectory(appdataPath);
            }

            _level.Ids = GetIds(_levelObjectManager.EditorObjects);
            string fileJSON = JsonConvert.SerializeObject(_level);

            // Saves file to project folder for testing purposes will be changed on release
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "SaveFiles\\" + _level.Name + ".json";
            File.WriteAllText(filePath, fileJSON);
        }
    }

    private void _saveAsButton_Clicked(object sender, EventArgs e) {
        GamestateManager.AddGamestate(new SaveAs(new Level("", TimeSpan.Zero, GetIds(_levelObjectManager.EditorObjects))));
    }

    private int[,] GetIds(EditorObject[,] editorObjects) {
        // converts the objects 2D array into JSON
        int[,] ids = new int[48, 20];
        for (int i = 0; i < 48; i++) {
            for (int j = 0; j < 20; j++) {
                ids[i, j] = editorObjects[i, j].Id;
            }
        }

        return ids;
    }
}
