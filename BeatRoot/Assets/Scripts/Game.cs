using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
  public GameState initialState;

  public GameState state;

  // Start is called before the first frame update
  void Start()
  {
    initialState.OnEnter();
    state = initialState;
  }

  // Update is called once per frame
  void Update()
  {

  }
}
