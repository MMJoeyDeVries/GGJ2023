using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GameGame : MonoBehaviour
{
  public GameState initialState;

  // Start is called before the first frame update
  void Start()
  {
    initialState.OnEnter();
  }

  // Update is called once per frame
  void Update()
  {

  }
}
