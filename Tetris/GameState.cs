﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class GameState
    {
        private Block currentBlock;
        public Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();
            }
        }
        public GameGrid GameGrid { get; }
        public BlockQueue BlockQueue { get; }
        public bool GameOver { get; private set;  }

        public GameState()
        {
            GameGrid = new GameGrid(22,11);
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.GetandUpdate();
        }
        private bool BlockFits()
        {
            foreach(Position p in CurrentBlock.TilePositions())
            {
                if(!GameGrid.IsEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }
            return true;
        }
        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();
            if(!BlockFits())
            {
                CurrentBlock.RotateCWW();
            }
        }
        public void RoteBlockCWW()
        {
            CurrentBlock.RotateCWW();
            if(!BlockFits())
            {
                CurrentBlock.RotateCW();
            }
        }
        public void MoveBackLeft()
        {
            CurrentBlock.Move(0, -1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }
        public void MoveBackRight()
        {
            CurrentBlock.Move(0, 1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }
        public bool IsGameOver()
        {
            return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
        }
        private void PlaceBlock()
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                GameGrid[p.Row, p.Column] = CurrentBlock.Id;
            }
            GameGrid.ClearFullRows();
            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = BlockQueue.GetandUpdate();
            }
            Draw(this);

        }
        public void MoveBlockDown()
        {
            CurrentBlock.Move(1,0);
            if (!BlockFits())
            {
                CurrentBlock.Move(-1,0);
                PlaceBlock();
            }
        }
    }
}
