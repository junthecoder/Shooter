using UnityEngine;
using System.Collections;

public class InformationText : MonoBehaviour {

	string text =
			"W S A D :  �ړ�\n" +
			"SHIFT : �_�b�V��\n" +
			"���N���b�N : ����\n" +
			"�E�N���b�N : �Y�[��\n" +
			"R : �����[�h\n" +
			"1 2 3 4: ����؂�ւ�\n" +
			"G : �O���l�[�h\n" +
			"F : �i�C�t\n" +
			"V : �ˌ����[�h�؂�ւ�\n" +
			"X : ���Ⴊ��\n" +
			"Z : �قӂ��O�i\n" +
			"ESC: �|�[�Y���j���[\n" +
			"F1:";

	void Start()
	{
		guiText.enabled = Options.main.showHelp;
		guiText.text = text;
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			Options.main.showHelp = !Options.main.showHelp;
			guiText.enabled = Options.main.showHelp;
			Options.main.Save();
		}

		guiText.text = string.Format("{0:f1}\n", 1 / Time.deltaTime) + text;
	}
}
