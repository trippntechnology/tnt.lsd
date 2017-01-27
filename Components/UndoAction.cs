using System.Collections.Generic;
using TNT.LSD.Objects;

namespace LSDComponents
{
	using TNTObjectList = List<TNTObject>;

	public class UndoAction
	{
		public enum UndoActionType { uaAdd, uaInsert, uaDelete, uaModify };

		#region Members

		private UndoActionType m_UndoAction;
		private TNTObject m_CopyOfObject;
		private TNTObject m_Object;
		private TNTObjectList m_ObjectList;

		#endregion

		public UndoAction(UndoActionType undoAction, TNTObject obj, TNTObjectList objList)
		{
			m_UndoAction = undoAction;
			m_Object = obj;
			m_CopyOfObject = obj.CreateUndoCopy();
			m_ObjectList = objList;
		}

		public UndoAction(UndoAction undoAction)
		{
			m_UndoAction = undoAction.m_UndoAction;
			m_CopyOfObject = undoAction.m_Object.CreateUndoCopy();
			m_Object = undoAction.m_Object;
			m_ObjectList = undoAction.m_ObjectList;
		}

		public void Execute()
		{
			switch (m_UndoAction)
			{
				case UndoActionType.uaAdd:

					// Add to list
					m_ObjectList.Add(m_Object);
					break;

				case UndoActionType.uaInsert:

					// Insert to list
					m_ObjectList.Insert(0, m_Object);
					break;

				case UndoActionType.uaDelete:

					// Remove from list
					m_ObjectList.Remove(m_Object);
					break;

				case UndoActionType.uaModify:

					// Restore state
					m_Object.Assign(m_CopyOfObject);
					break;
			}
		}
	}
}
