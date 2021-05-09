# AM1ObjectPool
汎用オブジェクトプールスクリプト。

## 構成
- AM1ObjectPool.cs
  - オブジェクトの生成と提供、改修を行うScriptableObject
- ObjectPoolInitiator.cs
  - AM1ObjectPoolから生成したアセットをゲームオブジェクトに保持させて、初期化するためのスクリプト
- Poolable.cs
  - AM1ObjectPoolで管理するオブジェクトにアタッチするMonoBehaviourのサブクラス

## オブジェクトプールの準備
1. プールさせるオブジェクトの作成
   1. 新規スクリプトを作成するか、オブジェクトを制御するスクリプトを開いて、`Poolable`を継承します
   1. 必要に応じて`Spawn()`や`Despawn()`をオーバーライドします
   1. 指定座標への生成と回収のみなら`Poolable`をオブジェクトにアタッチするだけでも使えます
1. AM1ObjectPoolアセットの作成
   1. Projectウィンドウの + から AM1 > Object Pool Asset を選択して、オブジェクトプールアセットを作成して、適当な名前にしておきます
   1. 作成したScriptableObjectに、プールさせるオブジェクトのプレハブ(`Poolable`スクリプトか`Poolable`を継承したスクリプトがアタッチされていること)と、管理する上限数を設定します
1. プール管理オブジェクトの作成
   1. シーンにオブジェクトプールを管理するためのゲームオブジェクトを作成します
   1. 作成したゲームオブジェクトに`ObjectPoolInitiator`スクリプトをアタッチします
   1. 管理させたいオブジェクトプールのScriptableObjectアセットをInspectorにアタッチします
   1. 設定したオブジェクトプールが必要とするオブジェクトがこのオブジェクトの子供として生成されます。オブジェクトを分ける必要がなければ1つのObjectPoolInitiatorに管理したい全てのオブジェクトプールのアセットをアタッチして構いません

以上で設定完了です。

## オブジェクトプールの利用
設定できたら、以下のようなスクリプトでオブジェクトプールからオブジェクトを取り出して利用します。

```cs
public class Spawner : MonoBehaviour {
    [Tooltip("オブジェクトプール"), SerializeField]
    AM1ObjectPool poolInstance = default;

    public void Spawn() {
        var obj = poolInstance.Get();
        if (obj != null) {
            obj.Spawn(poolInstance, transform.position);
        }
    }
}
```

オブジェクトを消す処理は通常はオブジェクト自身に持たせます。例えば何かに当たったら消えるような場合は以下のようなスクリプトを作成してオブジェクトにアタッチします。

```cs
public class CollisionDestroy : MonoBehaviour {
    void OnCollisionEnter(Collision other) {
        var poolable = GetComponent<Poolable>();
        if (poolable != null) {
            poolable.Despawn();
        }
        else {
            Destroy(gameObject);
        }
    }
}
```

## License
[MIT License](./LICENSE)

Copyright (C) 2021 YuTanaka


[EOF]
