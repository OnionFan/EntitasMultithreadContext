using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CreateEntitySystem : IInitializeSystem
{
    private readonly GameContext _context;

    public CreateEntitySystem(GameContext context)
    {
        _context = context;
    }
    
    public void Initialize()
    {
        for (int i = 0; i < 100; i++)
        {
            var entity = _context.CreateEntity();

            entity.AddPosition(new Vector3Int(0, 0, 0));
        }
    }
}
