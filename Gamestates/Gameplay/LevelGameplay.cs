using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;

namespace CS_Coursework;

public class LevelGameplay : Gamestate {
    private ButtonBar _navBar;
    private Button _restartLevelButton, _pauseLevelButton, _editLevelButton, _backButton;
    private PlayerCharacter _playerCharacter = new PlayerCharacter(new Vector2(100, 200));

    private GameplayObjectManager _gameplayObjectManager = new GameplayObjectManager();

    public override void LoadContent() {
        int pad = 32;
        int height = 50;
        int width = Game1.SCREEN_WIDTH - 2 * pad;

        // setup and positioning of buttons
        _navBar = new ButtonBar(new Vector2(pad, pad), width, height);
        _restartLevelButton = new Button(120, 40, "Restart");
        _pauseLevelButton = new Button(120, 40, "Pause");
        _editLevelButton = new Button(120, 40, "Edit");
        _backButton = new Button(120, 40, "Back");

        // applying events to buttons
        _restartLevelButton.Clicked += _restartLevelButton_Clicked;
        _pauseLevelButton.Clicked += _pauseLevelButton_Clicked;
        _editLevelButton.Clicked += _editLevelButton_Clicked;
        _backButton.Clicked += _backButton_Clicked;

        // adding buttons to button bar
        _navBar.Buttons = new List<Button>() { _restartLevelButton, _pauseLevelButton, _editLevelButton, _backButton };
        _navBar.SetButtonPositions();

        AddObject(_navBar);

        ButtonBar test = new ButtonBar(new Vector2(32, 90), 1536, 640);
        test.Buttons = new List<Button>();
        AddObject(test);

        loadLevelData();

        AddObject(_playerCharacter);
        for (int i = 0; i < 48; i++) {
            for (int j = 0; j < 16; j++) {
                if (_gameplayObjectManager.LevelObjects[i, j].Visible) {
                    _playerCharacter.Colliders.Add(_gameplayObjectManager.LevelObjects[i, j].BoundingBox);
                }
            }
        }
    }

    public override void Update(GameTime gameTime) {
        base.Update(gameTime);
    }

    private void _backButton_Clicked(object sender, EventArgs e) {
        GamestateManager.RemoveGamestate();
    }

    private void _restartLevelButton_Clicked(object sender, EventArgs e) {
        Debug.WriteLine("Level loaded for testing");
    }

    private void _pauseLevelButton_Clicked(object sender, EventArgs e) {
        Debug.WriteLine("Publish level button pressed");
    }

    private void _editLevelButton_Clicked(object sender, EventArgs e) {
    }

    private void loadLevelData() {
        // loads the json from the file
        string filePath = AppDomain.CurrentDomain.BaseDirectory + "SaveFiles\\test.json";
        string levelJson = File.ReadAllText(filePath);

        // sets the cell-labels and the level objects from the json
        _gameplayObjectManager.PopulateCells(this, JsonConvert.DeserializeObject<int[,]>(levelJson));
    }
}
