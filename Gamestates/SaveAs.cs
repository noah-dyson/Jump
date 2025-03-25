using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Security.Principal;

namespace CS_Coursework;

public class SaveAs : Gamestate {
    public static GameWindow Window;
    private int[,] _ids;
    private ButtonBar _optionBar, _textBoxBar;
    private Button _backButton, _saveButton;
    private TextBox _textBox;

    public SaveAs(int[,] ids) {
        _ids = ids;
    }

    public override void LoadContent() {
        int pad = 50;
        int height = 280;
        int width = Game1.SCREEN_WIDTH - 2 * pad;

        _optionBar = new ButtonBar(new Vector2(pad, Game1.SCREEN_HEIGHT - pad - height), width, height);
        _backButton = new Button(320, 220, "Back");
        _saveButton = new Button(320, 220, "Save");

        _optionBar.Buttons = new List<Button>() { _saveButton, _backButton };
        _optionBar.SetButtonPositions();

        _backButton.Clicked += BackButton_Clicked;
        _saveButton.Clicked += SaveButton_Clicked;

        _textBoxBar = new ButtonBar(new Vector2(pad, height / 2), width, height);
        _textBoxBar.Buttons = new List<Button>() { };

        _textBox = new TextBox(new Vector2(pad * 2, height / 2 + pad), width - pad * 2, height - pad * 2);
        Window.TextInput += _textBox.HandleTextInput;
        AddObject(_textBoxBar);
        AddObject(_textBox);

        AddObject(_optionBar);
    }

    private void BackButton_Clicked(object sender, EventArgs e) {
        GamestateManager.RemoveGamestate();
    }

    private void SaveButton_Clicked(object sender, EventArgs e) {
        string fileJSON = JsonConvert.SerializeObject(_ids);

        // Saves file to project folder for testing purposes will be changed on release
        string filePath = AppDomain.CurrentDomain.BaseDirectory + "SaveFiles\\" + _textBox.Text + ".json";
        File.WriteAllText(filePath, fileJSON);

        GamestateManager.RemoveGamestate();
    }
}