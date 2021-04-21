using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Entitas;
using UnityEngine;

namespace Common
{
    public class GameRunner : MonoBehaviour
    {
        private CancellationTokenSource _cancellationTokenSource;

        private const int ITERATION_COUNT = 50;

        private const int COUNT_THREADS = 8;
        
        List<Task<int>> tasks = new List<Task<int>>(COUNT_THREADS);

        void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            for (int i = 0; i < COUNT_THREADS; i++)
            {
                var count = i;

                tasks.Add(Task.Run((async () =>
                {
                    var result = await RunContext(count + 1);

                    return result;
                })));
            }
        }

        private void Update()
        {
            foreach (var task in tasks)
            {
                if (task.IsCompleted)
                {
                    Debug.Log(task.Result);
                }
            }
        }

        private static async Task<int> RunContext(int number)
        {
            GameContext context = new GameContext();

            GameMatcher matcher = new GameMatcher(new MatcherInstance<GameEntity>());

            var entity = context.CreateEntity();
            
            entity.AddPosition(new Vector3Int(0, 0, 0));

            var systems = new Systems().Add(new CreateEntitySystem(context)).Add(new MoveEntitySystem(matcher, context, number));
            
            systems.Initialize();

            int iterationCount = 0;

            int endIterationCount = ITERATION_COUNT * number;

            while (iterationCount < endIterationCount)
            {
                systems.Execute();

                iterationCount++;
                
                await Task.Delay(20);
            }

            return number;
        }

        private void OnApplicationQuit()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}