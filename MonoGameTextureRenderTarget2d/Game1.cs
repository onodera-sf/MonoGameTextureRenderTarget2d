using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTextureRenderTarget2d
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		
		/// <summary>描画するテクスチャー画像。</summary>
		private Texture2D _texture;

		/// <summary>描画対象となるレンダーターゲット。</summary>
		private RenderTarget2D _renderTarget;

		/// <summary>テクスチャーを回転させる角度。</summary>
		private float _rotate;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;

			// ゲームの解像度の設定
			_graphics.PreferredBackBufferWidth = 1280;
			_graphics.PreferredBackBufferHeight = 720;
		}

		protected override void Initialize()
		{
			// TODO: ここに初期化ロジックを追加します

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: this.Content を使用してゲーム コンテンツをここに読み込みます

			// コンテンツからテクスチャーを読み込みます
			_texture = Content.Load<Texture2D>("Texture");

			_renderTarget = new RenderTarget2D(GraphicsDevice, 540, 360);
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: ここに更新ロジックを追加します

			// テクスチャーの回転角度を更新します
			_rotate += 0.01f;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			// TODO: ここに描画コードを追加します

			// 描画先対象をレンダーターゲットに設定
			GraphicsDevice.SetRenderTarget(_renderTarget);

			// テクスチャーの情報をクリアし単色で塗りつぶします
			GraphicsDevice.Clear(Color.LightYellow);

			// スプライトバッチを使用してスプライトを書き込みます
			// レンダーターゲットの外に影響ないことを確認するためにわざと領域をはみ出るように描画しています
			_spriteBatch.Begin();
			for (int i = 0; i < 4; i++)
			{
				_spriteBatch.Draw(_texture, new Vector2((i / 2) * 200 + 32, (i % 2) * 200 + 32),
					null, Color.White, _rotate * i, new Vector2(64, 64), 1, SpriteEffects.None, 0);
			}
			_spriteBatch.End();

			// レンダーターゲットを解除します。これ以降の描画処理はメインウインドウに対して行われます
			GraphicsDevice.SetRenderTarget(null);

			// メインウィンドウの描画情報をクリアし単色で塗りつぶします
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// メインウィンドウに対してレンダーターゲットをテクスチャーとして描画します
			_spriteBatch.Begin();
			_spriteBatch.Draw(_renderTarget, new Vector2(400, 400), null, Color.White,
				_rotate, new Vector2(_renderTarget.Width / 2, _renderTarget.Height / 2), 1, SpriteEffects.None, 0);
			_spriteBatch.End();

			base.Draw(gameTime);
		}

	}
}