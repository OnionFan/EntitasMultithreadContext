using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class MoveEntitySystem : IExecuteSystem
{
    private readonly GameMatcher _gameMatcher;
    private readonly GameContext _context;

    private IGroup<GameEntity> allPositions;

    private int number;
    
    public MoveEntitySystem(GameMatcher gameMatcher, GameContext context, int number)
    {
        _gameMatcher = gameMatcher;
        _context = context;

        allPositions = context.GetGroup(gameMatcher.AllOf(gameMatcher.Position));

        this.number = number;
    }

    public void Execute()
    {
        foreach (var position in allPositions)
        {
            //position.ReplacePosition(position.position.Pos + new Vector3Int(1, 1, 1));
            
            position.ReplacePosition(new Vector3Int(number, number, number));
        }
    }
}
