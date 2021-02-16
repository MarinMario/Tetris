using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Tetris {
    class TetrisGame {
        Texture2D block;
        Texture2D border;

        Point gridSize = new Point(8, 16);
        Point cellSize = new Point(30, 30);

        Vector2 position = new Vector2(3, 0);
        string[][] tetromino;
        int rotation = 0;

        float placeBlocksTimer = 0f;
        List<Vector2> placedBlocks = new List<Vector2>{};

        public TetrisGame(Texture2D block, Texture2D border) {
            this.block = block;
            this.border = border;
            RandomTetromino();
        }

        public void Update(float delta){
            var lastPos = position;
            Move(delta);
            PlaceBlocks(delta, lastPos);
            ClearFullRows();
            RotateTetromino(delta);
        }

        public void Draw(SpriteBatch sb) {
            //draw grid
            for(var y = 0; y < gridSize.Y; y++)
                for(var x = 0; x < gridSize.X; x++)
                    sb.Draw(border, new Rectangle(new Point(x, y) * cellSize, cellSize), Color.Orange);
            
            //draw tetromino
            for(var y = 0; y < tetromino[rotation].Length; y++)
                for(var x = 0; x < tetromino[rotation][y].Length; x++)
                    if (tetromino[rotation][y][x] == 'X') {
                        var pos = (position + new Vector2(x, y)).ToPoint() * cellSize;
                        sb.Draw(block, new Rectangle(pos, cellSize), Color.LightBlue);
                    }

            //draw placed blocks
            foreach(var pos in placedBlocks)
                sb.Draw(block, new Rectangle(pos.ToPoint() * cellSize, cellSize), Color.LightBlue);
        }

        float gravityTimer = 0f;
        float moveTimer = 0f;
        float gravityTimeOut = 1f;
        void Move(float delta) {
            var collision = Collision(tetromino, position, rotation);
            var input = Keyboard.GetState();

            //current tetromino move down
            gravityTimer += delta;
            if (gravityTimer >= gravityTimeOut && !collision["bottom"]) {
                gravityTimer = 0;
                position.Y += 1;
            }
            if (input.IsKeyDown(Keys.S))
                gravityTimeOut = 0.1f;
            else gravityTimeOut = 1f; 

            //current tetromino move left-right
            moveTimer += delta;
            if (moveTimer > 0.1) {
                if (input.IsKeyDown(Keys.D) && !collision["right"]) {
                    position.X += 1;
                    moveTimer = 0;
                }
                if (input.IsKeyDown(Keys.A) && !collision["left"]) {
                    position.X -= 1;
                    moveTimer = 0;
                }
            }
        }

        Dictionary<string, bool> Collision(string[][] tetromino, Vector2 pos, int rot) {
            var collision = new Dictionary<string, bool> {
                {"right", false},
                {"left", false},
                {"bottom", false},
                {"badCollision", false}
            };

            for(var y = 0; y < tetromino[rot].Length; y++)
                for(var x = 0; x < tetromino[rot][y].Length; x++) {
                    var blockPos = new Vector2(x, y) + pos;
                    if (tetromino[rot][y][x] == 'X') {
                        if (blockPos.X < 0 || blockPos.X >= gridSize.X || blockPos.Y >= gridSize.Y)
                            collision["badCollision"] = true;
                        if (blockPos.X <= 0)
                            collision["left"] = true;
                        if (blockPos.X >= gridSize.X - 1)
                            collision["right"] = true;
                        if (blockPos.Y >= gridSize.Y - 1)
                            collision["bottom"] = true;

                        foreach (var placedBlock in placedBlocks) {
                            if (blockPos == placedBlock)
                                collision["badCollision"] = true;
                            if (blockPos.X - placedBlock.X == 1 && blockPos.Y == placedBlock.Y)
                                collision["left"] = true;
                            if (blockPos.X - placedBlock.X == -1 && blockPos.Y == placedBlock.Y)
                                collision["right"] = true;
                            if (blockPos.Y - placedBlock.Y == -1 && blockPos.X == placedBlock.X)
                                collision["bottom"] = true;
                        }
                }
            }
            return collision;
        }

        void PlaceBlocks(float delta, Vector2 lastPos) {
            if (position == lastPos)
                placeBlocksTimer += delta;
            else placeBlocksTimer = 0;
            if (placeBlocksTimer < 1)
                return;
            
            for (var y = 0; y < tetromino[rotation].Length; y++)
                for (var x = 0; x < tetromino[rotation][y].Length; x++)
                    if (tetromino[rotation][y][x] == 'X')
                        placedBlocks.Add(new Vector2(x, y) + position);

            rotation = 0;
            RandomTetromino();
            position = new Vector2(3, -3);

        }

        void ClearFullRows() {
            //traverses every row in the grid
            for (var y = 0; y < gridSize.Y; y++) {
                var mightRemove = new List<Vector2>{};
                //adds the blocks that are on the same row to the list
                foreach (var block in placedBlocks)
                    if (block.Y == y)
                        mightRemove.Add(block);
                
                //if the mightRemove list has as many elements as there are cells in a row that means
                //the row is full so it removes all the elements in that row
                if (mightRemove.Count >= gridSize.X) {
                    foreach (var block in mightRemove)
                        placedBlocks.Remove(block);
                    //moves every row above the one that just got removed one row lower
                    for (var i = 0; i < placedBlocks.Count; i++)
                        if (placedBlocks[i].Y < y)
                            placedBlocks[i] = new Vector2(placedBlocks[i].X, placedBlocks[i].Y + 1);
                }
            }
        }

        void RotateTetromino(float delta) {
            var testRotation = rotation;
            Input.GetState();
            if (Input.JustPressed(Keys.J))
                testRotation -= 1;
            if (Input.JustPressed(Keys.K))
                testRotation += 1;
        
            if (testRotation >= tetromino.Length)
                testRotation = 0;
            if (testRotation < 0)
                testRotation = tetromino.Length - 1;

            if (!Collision(tetromino, position, testRotation)["badCollision"]) {
                rotation = testRotation;
            } else {
                var wallKick = Tetromino.WallKick(rotation, testRotation);
                foreach(var elem in wallKick) {
                    var newPos = position + elem;
                    if (!Collision(tetromino, newPos, testRotation)["badCollision"]) {
                        position = newPos;
                        rotation = testRotation;
                        return;
                    }
                }   
            } 
        }

        Random random = new Random();
        void RandomTetromino() {
            var tetrominoList = new string[][][] { Tetromino.S, Tetromino.L, Tetromino.O };
            tetromino = tetrominoList[random.Next(tetrominoList.Length)];
        }
    }
}