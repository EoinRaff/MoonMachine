# MoonMachine
Game for MED6 course: Theory and Practice of Game Design and Development by Potato Babes development team

Notes for Developers:

First thing every time you work on the project, pull and resolve conflicts.

Always work in a new branch.
Always work in a new scene.

Never work on the same scene as another developer.
Never work on the same script as another developer.

Make sure to add descriptive comments to all commits.

ScriptingStyle:

    public variables at top of script
    private variables below public

    Update ONLY called in GameManager
    Input controls ONLY in InputManager

    variables should be in camelCase
    Functions should be PascalCase and braces on a new line
    Functions should be verbs that explain what they do
    
    e.g. 

    float myVariable

    public float MultiplyByTwo(float valueToBeMultiplied)
    {
        return valueToBeMultiplied * 2;
    }


