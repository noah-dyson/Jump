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
    private PlayerCharacter _playerCharacter = new PlayerCharacter();
    private Key _key;
    private Door _door;
    private bool _loaded = false;
    private Level _level;
    private KeyIcon _keyIcon;
    public KeyIcon KeyIcon { get { return _keyIcon; } }

    private GameplayObjectManager _gameplayObjectManager = new GameplayObjectManager();
    private Timer _timer = new Timer();
    private bool _testing = false;

    public LevelGameplay(Level level) {
        _level = level;
    }

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
        _backButton.Clicked += _backButton_Clicked;

        // adding buttons to button bar
        _navBar.Buttons = new List<Button>() { _restartLevelButton, _pauseLevelButton, _backButton };
        _navBar.SetButtonPositions();

        AddObject(_navBar);

        ButtonBar test = new ButtonBar(new Vector2(32, 90), 1536, 640);
        test.Buttons = new List<Button>();
        AddObject(test);

        _gameplayObjectManager.PopulateCells(this, _level.Ids, _playerCharacter);
        AddObject(_playerCharacter);
        for (int i = 0; i < 48; i++) {
            for (int j = 0; j < 20; j++) {
                // adds object to colliders if it is visible and it collides
                if (_gameplayObjectManager.LevelObjects[i, j].Visible && _gameplayObjectManager.LevelObjects[i, j].Collides) {
                    _playerCharacter.Colliders.Add(_gameplayObjectManager.LevelObjects[i, j]);
                }
            }
        }

        AddObject(_timer);
        _keyIcon = new KeyIcon(new Vector2(Game1.SCREEN_WIDTH - 160, Game1.SCREEN_HEIGHT - 160));
        AddObject(_keyIcon);

    }

    public override void Update(GameTime gameTime) {
        base.Update(gameTime);
    }

    private void _backButton_Clicked(object sender, EventArgs e) {
        GamestateManager.RemoveGamestate();
    }

    private void _restartLevelButton_Clicked(object sender, EventArgs e) {
        GamestateManager.RemoveGamestate();
        GamestateManager.AddGamestate(new LevelGameplay(_level));
    }

    private void _pauseLevelButton_Clicked(object sender, EventArgs e) {
        _timer.PauseTimer(_playerCharacter);
    }

    private void loadLevelData() {
        // loads the json from the file
        string filePath = AppDomain.CurrentDomain.BaseDirectory + "SaveFiles\\test.json";
        string levelJson = File.ReadAllText(filePath);

        // sets the cell-labels and the level objects from the json
        _gameplayObjectManager.PopulateCells(this, JsonConvert.DeserializeObject<Level>(levelJson).Ids, _playerCharacter);
    }

    public void LoadNewLevelData(int[,] levelIds, bool testing = false) {
        _loaded = true;
        _testing = testing;
        if (!testing) {
            loadLevelData();
        }
        _gameplayObjectManager.PopulateCells(this, levelIds, _playerCharacter);
    }

    public void UpdateTime() {
        if (!_testing) {
            // checks that best time is not 0 otherwise level not completed yet
            // if new time is better than old time update it
            if (_timer.Time < _level.BestTime || _level.BestTime == TimeSpan.Zero) {
                _level.BestTime = _timer.Time;
                // updates old time
                string fileJSON = JsonConvert.SerializeObject(_level);
                File.WriteAllText(_level.FilePath, fileJSON);
            }
        }
    }
}
