# Bedivere Knx Solution

[English readme document](README_en.md)

KNX智能照明控制软件，基于Knx.Falcon.Sdk。

> [!CAUTION]
> 本软件可能存在bug，不建议商用，否则后果自负。
>
> This software may have bugs and is not recommended for commercial use, otherwise the consequences will be borne by oneself.

## 许可证

本程序为自由软件， 在自由软件联盟发布的GNU通用公共许可协议的约束下，你可以对其进行再发布及修改。协议版本为第三版或（随你）更新的版本。

我们希望发布的这款程序有用，但不保证，甚至不保证它有经济价值和适合特定用途。详情参见GNU通用公共许可协议。

你理当已收到一份GNU通用公共许可协议的副本。如果没有，请查阅 [http://www.gnu.org/licenses/](http://www.gnu.org/licenses/)。

## 授权机制

本软件使用 `Ouroboros.AuthManager.Eos`进行授权，无需使用授权机制的情况下删除相关代码即可。

## 数据表格式

本软件使用Excel表格存储数据，下面是数据格式说明(下划线的数据类型来自于Knx.Falcon库)：

### 接口 Interfaces

此表用于存储KNX系统的接口，留空即全部使用KNX广播路由接口 `224.0.23.12:3671`。

| 列               | 类型                             | .NET类型                              | 说明     |
| ---------------- | -------------------------------- | ------------------------------------ | -------- |
| InterfaceCode    | Text                             | String                               | 接口编号 |
| InterfaceName    | Text                             | String                               | 接口名称 |
| InterfaceType    | *Sequence*<sup>1</sup>           | <ins>*ConnectorType*</ins>           | 接口类型 |
| InterfaceAddress | Text                             | String                               | 接口地址 |
| Port             | Number                           | Integer                              | 端口     |
| Enable           | *Sequence*<sup>2</sup>           | Boolean                              | 启用     |

- <sup>1</sup> KNX接口类型，IpRouting、IpTunneling、Usb。
- <sup>2</sup> FALSE/TRUE序列。

### 区域 Areas

此表存储KNX区域信息。

| 列         | 类型                         | .NET类型  | 说明      |
| ---------- | --------------------------- | -------- | ---------- |
| MainCode   | Text                        | String   | 主区域编号 |
| MainArea   | Text                        | String   | 主区域名称 |
| MiddleCode | Text                        | String   | 中区域编号 |
| MiddleArea | Text                        | String   | 中区域名称 |
| SubCode    | Text                        | String   | 子区域编号 |
| SubArea    | Text                        | String   | 子区域名称 |
| AreaCode   | General<sup>1</sup>         | String   | 区域编号   |

- <sup>1</sup> AreaCode列通过公式自动填充，格式为`MainCode.MiddleCode.SubCode`。

### 对象 Objects

此表存储KNX系统中的对象信息，每个对象均有开关控制、开关反馈、数值控制、数值反馈4个组。

| 列                                | 类型                             | .NET类型                              | 说明          |
| --------------------------------- | -------------------------------- | ------------------------------------ | ------------- |
| AreaCode<sup>1</sup>              | Text                             | String                               | 区域编号      |
| InterfaceCode<sup>2</sup>         | Text                             | String                               | 接口编号      |
| ObjectCode                        | Text                             | String                               | 对象编号      |
| ObjectType                        | *Sequence*                       | *KnxObjectType*<sup>3</sup>           | 对象类型      |
| ObjectName                        | Text                             | String                               | 对象名称      |
| Sw_GrpDpt                         | *Sequence*<sup>4</sup>           | <ins>*DptBase*</ins>                 | 开关-地址类型 |
| Sw_Ctl_GrpAddr                    | Text                             | <ins>*GroupAddress*</ins>            | 开关-控制地址 |
| Sw_Fdb_GrpAddr                    | Text                             | <ins>*GroupAddress*</ins>            | 开关-反馈地址 |
| Val_GrpDpt                        | *Sequence*<sup>4</sup>           | *DptBase*                            | 数值-地址类型 |
| Val_Ctl_GrpAddr                   | Text                             | <ins>*GroupAddress*</ins>            | 数值-控制地址 |
| Val_Fdb_GrpAddr                   | Text                             | <ins>*GroupAddress*</ins>            | 数值-反馈地址 |

- <sup>1</sup> 来自 `Areas`表的 `AreaCode`列。
- <sup>2</sup> 来自 `Interfaces`表的 `InterfaceCode`列，留空即为使用KNX广播路由接口。
- <sup>3</sup> *KnxObjectType*为BedivereKnx定义的枚举类型，表示KNX对象类型（Switch、Dimming、Scene等）。
- <sup>4</sup> KNX DataPointType的序列，来自 `Knx.Falcon.ApplicationData.DatapointTypes.DptFactory.AllDatapointTypes`。

### 场景 Scenes

此表存储KNX系统中的场景信息。

| 列                                | 类型 | .NET类型                             | 说明     |
| --------------------------------- | ---- | ----------------------------------- | -------- |
| AreaCode<sup>1</sup>              | Text | String                              | 区域编号 |
| InterfaceCode<sup>2</sup>         | Text | String                              | 接口编号 |
| SceneCode                         | Text | String                              | 场景编号 |
| SceneName                         | Text | String                              | 场景名称 |
| GroupAddress                      | Text | <ins>*GroupAddress*</ins>           | 组地址   |
| SceneValues<sup>3</sup>           | Text | String                              | 场景值   |

- <sup>1</sup> 来自 `Areas`表的 `AreaCode`列。
- <sup>2</sup> 来自 `Interfaces`表的 `InterfaceCode`列，留空即为使用KNX广播路由接口。
- <sup>3</sup> 格式为 `{场景值1}={场景名1},{场景值2}={场景名2}，……`，例：`0=全关,1=全开`。

### 设备 Devices

此表存储KNX系统中的设备信息，用于检查设备是否在线。

| 列                                | 类型 | .NET类型                                  | 说明     |
| --------------------------------- | ---- | ---------------------------------------- | -------- |
| AreaCode<sup>1</sup>              | Text | String                                   | 区域编号 |
| InterfaceCode<sup>2</sup>         | Text | String                                   | 接口编号 |
| DeviceCode                        | Text | String                                   | 设备编号 |
| DeviceName                        | Text | String                                   | 设备名称 |
| DeviceModel                       | Text | String                                   | 设备型号 |
| IndividualAddress                 | Text | <ins>*IndividualAddress*</ins>           | 设备地址 |

- <sup>1</sup> 来自 `Areas`表的 `AreaCode`列。
- <sup>2</sup> 来自 `Interfaces`表的 `InterfaceCode`列，留空即为使用KNX广播路由接口。

### 定时 Schedules

此表用于实现软件中的定时控制功能。

| 列             | 类型                             | .NET类型 | 说明         |
| -------------- | -------------------------------- | -------- | ------------ |
| ScheduleCode   | Text                             | String   | 定时编号     |
| ScheduleName   | Text                             | String   | 定时名称     |
| TargetType     | Text                             | String   | 目标对象类型 |
| TargetCode     | Text                             | String   | 目标对象编号 |
| ScheduleEvents | Text                             | String   | 定时事件     |
| Enable         | *Sequence*<sup>1</sup>           | Boolean  | 启用         |

- <sup>1</sup> FALSE/TRUE序列。

### 链接

此表用于存储KNX系统中使用的链接（例如带有网页登录入口的KNX逻辑控制器）。

| 列       | 类型 | .NET类型  | 说明     |
| -------- | ---- | -------- | -------- |
| LinkName | Text | String   | 链接名称 |
| LinkUrl  | Text | String   | 链接地址 |
| Account  | Text | String   | 账号名   |
| Password | Text | String   | 密码     |

## 图形文件格式

本软件使用[draw.io](https://www.drawio.com)来绘制图形界面。

### 关联字符串格式 Mapping String Format

绑定字符串格式为：`{绑定对象}{方向符号}{变换数值}{数据类型}`。

#### 关联对象 Mapping Object

- 组地址类型：直接输入组地址即可，比如 `1/2/101`。
- 数据表类型：格式为 `${ObjectCode}`，其中ObjectCode来自数据文件Objects表或Scene表的Code。

#### 方向符号 Direction Symbol

此项及之后的各项可以省略，默认值为 `@0|1#1.000`。

- `@`代表反馈
- `=`代表控制

#### 变换数值 Change Values

数值支持固定值、多值切换、范围值三种方式，此项及之后的各项可以省略，默认值为 `0|1#1.000`。

- 固定值直接输入值即可
- 多值切换中，用 `|`分隔不同的值
- 范围值用 `{最小值}~{最大值}`表示

#### 数据类型 Data Type

此处数据类型指的是KNX中的DPT类型数字，前面加 `#`，比如 `#1.001`、`#5.003`等，此项可以省略，数字量默认值为 `1.000`（布尔型），模拟量默认值为 `5.000`（8位无符号整型），模拟量类型不建议使用默认值。

#### 示例

- `1/1/101@0|1#1.0`：1/1/101组地址的反馈，DPT为1.000，在0/1之间变化
- `1/1/102`：1/1/102组地址的反馈，DPT为1.000，在0/1之间变化
- `1/1/1=1`：控制1/1/1组地址，DPT为1.000，控制值为1
- `1/2/200=5`：控制1/2/200组地址，DPT为5.000，控制值为5
- `1/2/201@0~100#5.003`：1/2/201组地址的反馈，DPT为5.003，在0~100之间变化
- （格式暂略，将在之后的版本中补充……）

## USB接口连接问题

连接USB接口时可能会报错“KnxUsbFix not installed”，安装KnxUsbFix修复即可。

详情参见：https://support.knx.org/hc/en-us/articles/115001443670-KNX-USB-interface-not-recognized

## 依赖项

- [Knx.Falcon.Sdk](https://help.knx.org/falconsdk)
- [DocumentFormat.OpenXml](https://github.com/dotnet/Open-XML-SDK)
- [Ouroboros.Hmi]
- [Ouroboros.Authorization]

## 鸣谢

非常感谢以下团体和个人对本项目的慷慨贡献和无私支持：

- [KNX协会](https://www.knx.org)
- [ASTON Technologie GmbH](https://www.aston-technologie.de/)
- [JGraph Ltd](https://www.drawio.com)
- [Embedded Systems, SIA](https://openrb.com/)
- [西门子股份公司](https://www.siemens.com)
- [德力西电气有限公司](https://www.delixi-electric.com/)
- [文心一言](https://yiyan.baidu.com/)
- 微信公众号：技术老小子
- Anonym van Tadig
- 罗志坚

<br>

版权所有 © 2024 Ouroboros Software Foundation。保留所有权利。
