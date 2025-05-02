using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Makina.Core;

namespace Sandbox;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private Engine _engine;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _engine = new Engine(GraphicsDevice, Content);
        base.Initialize();
        _engine.Initialize();
    }

    protected override void LoadContent()
    {
        _engine.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        _engine.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _engine.Draw(gameTime);
    }
}