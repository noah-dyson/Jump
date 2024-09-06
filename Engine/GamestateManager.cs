using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CS_Coursework;

public static class GamestateManager
{
    private static Stack<Gamestate> _gamestates = new Stack<Gamestate>();

    public static void AddGamestate(Gamestate gamestate)
    {
        _gamestates.Push(gamestate);
        CurrentGamestate().LoadContent();
    }
    public static void RemoveGamestate()
    {
        if (_gamestates.Count == 1)
        {
            throw new Exception("Cannot remove lone gamestate.");
        }
        _gamestates.Pop();
    }
    public static Gamestate CurrentGamestate()
    {
        return _gamestates.Peek();
    }
}