namespace Fractal

open System

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

type Game1 () as this =
    inherit Game()
 
    let graphics = new GraphicsDeviceManager(this)
    let mutable spriteBatch = Unchecked.defaultof<_>

    let mutable posX = 0;
    let mutable posX2 = 800;
    
    do
        this.Content.RootDirectory <- "Content"
        this.IsMouseVisible <- true

    let add a b =
        match b with
        |2 ->
            let c = 2
            let g = 3
            a - c
        |_ ->
            a + b

    override this.Initialize() =
        // TODO: Add your initialization logic here
        
        base.Initialize()

    override this.LoadContent() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)

        // TODO: use this.Content to load your game content here
 
    override this.Update (gameTime) =
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        then this.Exit();

        // TODO: Add your update logic here
    
        base.Update(gameTime)
 
    override this.Draw (gameTime) =
        this.GraphicsDevice.Clear Color.Black

        // Window size: 800 x 480

        // this.DrawLine(new Vector2(0.0f, 0.0f), new Vector2(800.0f, 480.0f));
        // this.DrawLine(new Vector2(400.0f, 27.0f), new Vector2(100.0f, 460.0f));
        // this.DrawLine(new Vector2(400.0f, 27.0f), new Vector2(100.0f, 470.0f));
        // this.DrawLine(new Vector2(400.0f, 27.0f), new Vector2(100.0f, 480.0f));
        // this.DrawLine(new Vector2(400.0f, 27.0f), new Vector2(100.0f, 490.0f));

        //this.DrawTriangle(new Vector2(200.0f, 413.0f), new Vector2(400.0f, 67.0f), new Vector2(600.0f, 413.0f));
        this.Sierpinski(new Vector2(200.0f, 413.0f), new Vector2(400.0f, 67.0f), new Vector2(600.0f, 413.0f), 4); // Equilateral

        //this.Sierpinski(new Vector2(100.0f, 400.0f), new Vector2(300.0f, 300.0f), new Vector2(200.0f, 200.0f), 5); // Random

        // Learning functions
        // posX <- add posX 1
        // posX2 <- add posX2 2
        // this.DrawLine(new Vector2(float32 (posX), 0.0f), new Vector2(800.0f, 480.0f));
        // this.DrawLine(new Vector2(float32 (posX2), 0.0f), new Vector2(0.0f, 480.0f));

        base.Draw(gameTime)
    
    member this.Sierpinski(v0: Vector2, v1: Vector2, v2: Vector2, limit) =
        match limit with
        |0 ->
            ()
        |_ ->
            this.DrawTriangle(v0, v1, v2);

            let first = new Vector2((v0.X + v1.X) / 2.0f, (v0.Y + v1.Y) / 2.0f);
            let second = new Vector2((v1.X + v2.X) / 2.0f, (v1.Y + v2.Y) / 2.0f);
            let third = new Vector2((v2.X + v0.X) / 2.0f, (v2.Y + v0.Y) / 2.0f);

            this.Sierpinski(v0, first, third, limit - 1);
            this.Sierpinski(first, v1, second, limit - 1);
            this.Sierpinski(third, second, v2, limit - 1);

            ()

    member this.DrawTriangle(v0: Vector2, v1: Vector2, v2: Vector2) =
        // this.DrawLine(v0, v1);
        // this.DrawLine(v1, v2);
        // this.DrawLine(v2, v0);
        this.DrawLine(v0, v1);
        this.DrawLine(v0, v2);
        this.DrawLine(v1, v2);

        ()

    member this.DrawLine(v1: Vector2, v2: Vector2) =
        let angle = MathF.Atan2(v2.Y - v1.Y, v2.X - v1.X);
        let distance = Vector2.Distance(v1, v2);
        let thickness = 2;
        
        spriteBatch.Begin();
        let texture = new Texture2D(this.GraphicsDevice, 1, 1);
        texture.SetData([|Color.White|]);
        spriteBatch.Draw(texture, new Rectangle(int (v1.X), int (v1.Y), int (distance), thickness), System.Nullable(), Color.White, angle, Vector2.Zero, SpriteEffects.None, 0.0f);
        spriteBatch.End();

        ()