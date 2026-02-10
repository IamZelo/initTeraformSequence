## The idea
Our project aims to gamify coding set in a futuristic space environment where user programs robot to make an inhabitable planet suitable for human life. We were inspired by how traditional text based coding often feels abstract and monotonous for beginners, we wanted to create something where learner can see the impact of their code in real time through robot actions.

## Goal
The code is transformed into visible actions performed by robot i.e the robot explores terrain, farms resources, mines and build sustainable ecosystem based on user’s logic.
The goal : Resources and energy will be limited and therefore the user must tinker and come up with an optimal solution otherwise they will run out of resources and the game will fail. The difficulty of game will increase as they level up.
Game focuses on problem solving skill rather than learning syntaxes of a language.
## Implementation and Challenges
The game will have visual coding blocks that must be connected together to perform a task and the user will be able to visualize their code with robot actions simultaneously. The code block will parsed by backend into robot commands. 
The key challenge will be building an interpreter to translate the code block code into actual commands to be performed by robot which will include using AST(abstract syntax tree) and linked list.

## Preview photos
<img width="1387" height="774" alt="image" src="https://github.com/user-attachments/assets/b7ec595b-7bfd-4ffb-a422-2cdf18e80495" />
Level (Farm)
<img width="2559" height="1304" alt="image" src="https://github.com/user-attachments/assets/335a81f0-b0c2-42a6-8150-92c4cbcd6c1c" />
<img width="2559" height="1358" alt="image" src="https://github.com/user-attachments/assets/02c6b3af-627a-4664-a1cd-baaa51c02d48" />

## Future Scope
<li>Add if-else and loop statements in codeblock system using doubly linked list.  </li>
<li>Create more levels like factory and teach user about arrays using conveyor belt. </li>
<li>Make the user harvest the best plants under limited energy constraint. (Apply knapsack algorithm)</li>
<li>Create a level with obstacles and reach the end point using minimum energy. (Apply shortest path finding)</li>
