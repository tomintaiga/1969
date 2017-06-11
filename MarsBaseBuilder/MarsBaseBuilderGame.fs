﻿namespace MarsBaseBuilder

open System

open Microsoft.Xna.Framework

open MarsBaseBuilder.Textures

type MarsBaseBuilderGame() as this =
    inherit Game()

    let graphicsContext = lazy (new GraphicsContext(this.GraphicsDevice))

    let mutable mission = GameLogic.newMission
    let mutable textures = Unchecked.defaultof<TextureContainer>

    override this.LoadContent() =
        let content = this.Content
        content.RootDirectory <- "resources"
        textures <- Textures.load content

    override __.Draw(gameTime) =
        use draw = graphicsContext.Value.BeginDraw()
        Renderer.apply draw (Renderer.commands textures mission)
        base.Draw(gameTime)

    override __.Update(gameTime) =
        mission <- GameLogic.update mission gameTime
        ()
    
    override __.Dispose(disposing: bool) =
        if disposing && graphicsContext.IsValueCreated
        then (graphicsContext.Value :> IDisposable).Dispose()
