# ゲーム概要

## サンプルプロジェクトの概要
- 2年生用の共同開発と設計例
- マウスカーソルの方向を向く
- クリックでショット
- ショットを撃つと反動で逆方向に加速
- 敵とブロックを配置
- 敵もプレイヤーと同じ操作系
- 弾が当たると敵は爆発
- 全部倒すとクリア
- 敵にぶつかるとゲームオーバー
- 開始待機、ゲーム、ゲームオーバー、クリア
- スコア

## 基本設計
- 入力はAM1Inputで取りまとめ。入力に関するハブとして機能し、処理済みの入力に関する情報を外部へ提供する
- 移動機能のMoverクラスは、指定の方向を向くIHeadToPointと向いている方向に加速するIAddForwardを実装させる
- ダメージを与えるIAttacker
- ダメージを受けるIDamageable
- 壁で消えるBlockDestroy

## オブジェクトプール
### 再検討：オブジェクトプールをプール対象オブジェクトに持たせる
- 生成するプレハブが必要
- オブジェクトの種類ごとにプールへのアクセスポイントが必要
- オブジェクト自身にstaticで実装 or ScriptableObjectにする or クラスを定義する
  - オブジェクト自身にstaticで実装だとプレハブや親オブジェクトの持たせ方、上限管理が難しい
  - ScriptableObjectだと生成先の親の指定ができない。呼び出し元にやらせる
  - クラスの定義は毎回クラスファイルの生成が面倒
- オブジェクトプールとオブジェクトの制御はアクターが異なるので、ScriptableObjectにプール機能をまとめて実装する。これにより弾のSpawn()を共通化する親クラスが用意できる

### 実装
- 弾の撃ち方を制御するShooter、弾を管理するObjectPool、プーリングできる弾の3つのアクターで制御
- ShooterはScriptableObjectをInspectorから受け取り、プールから弾を取り出して発射
- シーンの開始時に、ObjectPoolのInit(Transform parent)を呼び出してオブジェクトを生成。生成済みなら何もしない。この時に親オブジェクトを指定
- NormalShooterなどからScriptableObjectのインスタンス`.Spawn();`でインスタンスを取得。戻り値を必要な弾のクラスに変換して続けて発射のための処理を実行
- 定義
  - public class Bullet : IPoolable
  - public abstract class AM1ObjectPool<T> : MonoBehaviour where T new()
- AM1ObjectPool : ScriptableObjectの機能
  - 生成するIPoolableのプレハブ、プール上限数を定義
  - public List<IPoolable> ObjectPoolとUsingPoolを宣言
  - public Init(Transform parent)で必要ならプールの生成
  - public void DespawnAll()全てのオブジェクトをプールに戻す
  - public T Spawn()でオブジェクトを返す。余っているオブジェクトがなければnull
  - public void Despawn(IPoolable)で指定のオブジェクトをプールから解除

- 弾や爆発はオブジェクトプールAM1ObjectPoolで管理
- オブジェクトの種類ごとにAM1ObjectPoolのScriptableObjectを生成して値を設定して、ShooterやSpawnerにインスタンスを渡しておく
- 弾は??ShooterのScriptableObjectで作成。弾を撃つShot(IShootable)に弾の打ち方を実装。作成したアセットを、弾を撃つオブジェクトにInspectorから設定
- IShootableはプレイヤーや敵などに実装して、弾を撃つのに必要なパラメーターを返すメソッドを定義
- 弾自体は??Shooterとセットで運用。必要なパラメーターを??Shooterから受け取って、Spawn()とDespawn()を実装
- PoolableクラスはBulletなどのベースクラスとなるMonoBehaviourを継承した抽象クラスで、Despawn()を必須

## シーン管理
- 小作品なのでシーンは読みっぱなしにする
- フェード
- 初期化、非表示処理

## プレイヤー
- 操作に従ってIMoverとIShooterに指示を与えるPlayerController

## 敵
- 索敵して、近くのプレイヤーや敵に向かってショットするEnemyController00

## ショット
- IDamageableを持ち、撃った本人でなければダメージを与える
- ダメージを与えるIAttacker
- ダメージを受けるIDamageable

## 爆発
- オブジェクトプーリング

## 開発アレンジ
- 攻撃と防御のバランス設定
