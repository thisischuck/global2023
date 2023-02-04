using UnityEngine;

public class DescriptionBasedSO : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField][TextArea] string _description;
}
