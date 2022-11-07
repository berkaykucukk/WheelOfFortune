using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWithID
{
    public int ID { get; set; }
    void SetId(int id);
}