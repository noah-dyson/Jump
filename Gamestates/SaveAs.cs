using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Security.Principal;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Coursework;

public class SaveAs : Gamestate {
    private Level _level;
    private ButtonBar _optionBar, _textBoxBar;
    private Button _backButton, _saveButton;
    private TextBox _textBox;

    public SaveAs(Level level) {
        _level = level;
    }

    public override void LoadContent() {
        int pad = 50;
        int height = 280;
        int width = Game1.SCREEN_WIDTH - 2 * pad;

        // option bar contains buttons for saving and returning to previous screen
        _optionBar = new ButtonBar(new Vector2(pad, Game1.SCREEN_HEIGHT - pad - height), width, height);
        _backButton = new Button(320, 220, "Back");
        _saveButton = new Button(320, 220, "Save");

        _optionBar.Buttons = new List<Button>() { _saveButton, _backButton };
        _optionBar.SetButtonPositions();

        _backButton.Clicked += BackButton_Clicked;
        _saveButton.Clicked += SaveButton_Clicked;

        _textBoxBar = new ButtonBar(new Vector2(pad, height / 2), width, height);
        _textBoxBar.Buttons = new List<Button>() { };
        SpriteFont font = Game1.ContentManager.Load<SpriteFont>("SaveAs");

        _textBox = new TextBox(new Vector2(pad * 2, height / 2 + pad), width - pad * 2, height - pad * 2, font);
        // registers correct method to text input event
        Game1.CurrentWindow.TextInput += _textBox.HandleTextInput;
        AddObject(_textBoxBar);
        AddObject(_textBox);

        AddObject(_optionBar);
    }

    private void BackButton_Clicked(object sender, EventArgs e) {
        GamestateManager.RemoveGamestate();
    }

    private void SaveButton_Clicked(object sender, EventArgs e) {
        // filepath generated with corresponding file name
        string appdataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Jump");
        if (!Directory.Exists(appdataPath)) {
            Directory.CreateDirectory(appdataPath);
        }

        string filePath = Path.Combine(appdataPath, _textBox.Text + ".json");
        Level newLevel = new Level(_textBox.Text, _level.BestTime, _level.Ids, filePath);
        // json generated from level and file written to
        string fileJSON = JsonConvert.SerializeObject(newLevel);
        File.WriteAllText(filePath, fileJSON);

        // if there is a file at the original files path
        // delete it as the level has been renamed
        if (File.Exists(_level.FilePath)) {
            File.Delete(_level.FilePath);
        }

        GamestateManager.RemoveGamestate();
    }
}