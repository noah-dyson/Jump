using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CS_Coursework;

public class ObjectSelector {
    public ButtonBar SelectorBar;
    public ButtonBar SelectorBar2;
    public int CurrentObject;

    public void InitializeSelector() {
        // pad is the distance between the edge of the screen and the button bar
        int pad = 32;
        int height = 70;
        int width = Game1.SCREEN_WIDTH - 2 * pad;

        // generates selector button bar
        SelectorBar = new ButtonBar(new Vector2(pad, Game1.SCREEN_HEIGHT - pad - height * 2 + 10), width, height);
        SelectorBar.Buttons = new List<Button>(8);

        // loops through numbers 0 to 7 for first row of objects, creates button and adds event
        for (int i = 0; i < 8; i++) {
            Button objectButton = new Button(140, 50, i.ToString(), i);
            objectButton.Clicked += _objectButton_Clicked;
            SelectorBar.Buttons.Add(objectButton);
        }
        SelectorBar.SetButtonPositions();

        SelectorBar2 = new ButtonBar(new Vector2(pad, Game1.SCREEN_HEIGHT - pad - height), width, height);
        SelectorBar2.Buttons = new List<Button>(8);

        for (int i = 8; i < 16; i++) {
            Button objectButton = new Button(140, 50, i.ToString(), i);
            objectButton.Clicked += _objectButton_Clicked;
            SelectorBar2.Buttons.Add(objectButton);
        }
        SelectorBar2.SetButtonPositions();
    }

    private void _objectButton_Clicked(object sender, EventArgs e) {
        // grabs the button the called the event to see which object it corresponds to
        Button button = (Button)sender;
        CurrentObject = button.Id;
    }
}
