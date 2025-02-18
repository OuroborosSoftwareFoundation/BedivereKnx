# BedivereKnx

KNX智能照明控制软件，基于Knx.Falcon.Sdk。

KNX intelligent lighting control software, based on Knx.Falcon.Sdk library.

> [!CAUTION]
> 本软件可能存在bug，不建议商用，否则后果自负。
> 
> This software may have bugs and is not recommended for commercial use, otherwise the consequences will be borne by oneself.

> [!WARNING]
> 对盗版、传播、使用[**ASTON Technologie**](https://www.aston-technologie.de/)公司**WinSwitch**软件的组织和个人予以强烈谴责。
> 
> We issue a stern condemnation against all organizations and individuals who piracy, distribution, or unauthorized use of **WinSwitch** software from [ASTON Technologie GmbH](https://www.aston-technologie.de/).

## 许可证 License

本程序为自由软件， 在自由软件联盟发布的GNU通用公共许可协议的约束下，你可以对其进行再发布及修改。协议版本为第三版或（随你）更新的版本。

This program Is free software: you can redistribute it And/Or modify it under the terms Of the GNU General Public License As published by the Free Software Foundation, either version 3 Of the License, Or (at your option) any later version.

我们希望发布的这款程序有用，但不保证，甚至不保证它有经济价值和适合特定用途。详情参见GNU通用公共许可协议。

This program Is distributed In the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty Of MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License For more details.

你理当已收到一份GNU通用公共许可协议的副本。如果没有，请查阅 [http://www.gnu.org/licenses/](http://www.gnu.org/licenses/)。

You should have received a copy Of the GNU General Public License along with this program. If Not, see [http://www.gnu.org/licenses/](http://www.gnu.org/licenses/).

## 授权机制 Authorization

本软件使用`Ouroboros.Authorization.Iris`进行授权，无需使用授权机制的情况下删除相关代码即可。

This software is authorized with `Ouroboros.Authorization.Iris`. If you don't need authorization , you can simply delete the relevant code and compile it.

## 数据表格式 Data File Format

本软件使用Excel表格存储数据，下面是数据格式说明(下划线的数据类型来自于Knx.Falcon库)：

BedivereKnx.Client uses Excel to store data. The following provides a detailed description of the data format (.NET Type with underline is from Knx.Falcon library):

### 接口 Interfaces

此表用于存储KNX系统的接口，留空即全部使用KNX广播路由接口`224.0.23.12:3671`。

|列|类型|.NET类型|说明|
|---|---|---|---|
|InterfaceCode|Text|String|接口编号|
|InterfaceName|Text|String|接口名称|
|InterfaceType|*Sequence*<sup>1</sup>|<ins>*ConnectorType*</ins>|接口类型|
|InterfaceAddress|Text|String|接口地址|
|Port|Number|Integer|端口|
|Enable|*Sequence*<sup>2</sup>|Boolean|启用|

- <sup>1</sup> KNX接口类型，IpRouting、IpTunnel、Usb。
- <sup>2</sup> FALSE/TRUE序列。

### 区域 Areas

|列|类型|.NET类型|说明|
|---|---|---|---|
|MainCode|Text|String|主区域编号|
|MainArea|Text|String|主区域名称|
|MiddleCode|Text|String|中区域编号|
|MiddleArea|Text|String|中区域名称|
|SubCode|Text|String|子区域编号|
|SubArea|Text|String|子区域名称|
|AreaCode|General<sup>1</sup>|String|区域编号|

- <sup>1</sup> AreaCode列通过公式自动填充，格式为“`MainCode`.`MiddleCode`.`SubCode`”。

### 对象 Objects

此表存储KNX系统中的对象信息，每个对象均有开关控制、开关反馈、数值控制、数值反馈4个组。

|列|类型|.NET类型|说明|
|---|---|---|---|
|AreaCode<sup>1</sup>|Text|String|区域编号|
|InterfaceCode<sup>2</sup>|Text|String|接口编号|
|ObjectCode|Text|String|对象编号|
|ObjectType|*Sequence*|*KnxGroupType*<sup>3</sup>|对象类型|
|ObjectName|Text|String|对象名称|
|Sw_GrpDpt|*Sequence*<sup>4</sup>|<ins>*DptBase*</ins>|开关-地址类型|
|Sw_Ctl_GrpAddr|Text|<ins>*GroupAddress*</ins>|开关-控制地址|
|Sw_Fdb_GrpAddr|Text|<ins>*GroupAddress*</ins>|开关-反馈地址|
|Val_GrpDpt|*Sequence*<sup>4</sup>|*DptBase*|数值-地址类型|
|Val_Ctl_GrpAddr|Text|<ins>*GroupAddress*</ins>|数值-控制地址|
|Val_Fdb_GrpAddr|Text|<ins>*GroupAddress*</ins>|数值-反馈地址|

- <sup>1</sup> 来自`Areas`表的`AreaCode`列。
- <sup>2</sup> 来自`Interfaces`表的`InterfaceCode`列，留空即为使用KNX广播路由接口。
- <sup>3</sup> *KnxGroupType*为BedivereKnx定义的枚举类型，表示KNX对象类型（Switch、Dimming、Scene等）。
- <sup>4</sup> KNX DataPointType的序列，来自`Knx.Falcon.ApplicationData.DatapointTypes.DptFactory.AllDatapointTypes`。

### 场景 Scenes

此表存储KNX系统中的场景信息。

|列|类型|.NET类型|说明|
|---|---|---|---|
|AreaCode<sup>1</sup>|Text|String|区域编号|
|InterfaceCode<sup>2</sup>|Text|String|接口编号|
|SceneCode|Text|String|场景编号|
|SceneName|Text|String|场景名称|
|GroupAddress|Text|<ins>*GroupAddress*</ins>|组地址|
|SceneValues<sup>3</sup>|Text|String|场景值|

- <sup>1</sup> 来自`Areas`表的`AreaCode`列。
- <sup>2</sup> 来自`Interfaces`表的`InterfaceCode`列，留空即为使用KNX广播路由接口。
- <sup>3</sup> 格式为`{场景值1}={场景名1},{场景值2}={场景名2}，……`，例：`0=全关,1=全开`。

### 设备 Devices

此表存储KNX系统中的设备信息，用于检查设备是否在线。

|列|类型|.NET类型|说明|
|---|---|---|---|
|AreaCode<sup>1</sup>|Text|String|区域编号|
|InterfaceCode<sup>2</sup>|Text|String|接口编号|
|DeviceCode|Text|String|设备编号|
|DeviceName|Text|String|设备名称|
|DeviceModel|Text|String|设备型号|
|IndividualAddress|Text|<ins>*IndividualAddress*</ins>|设备地址|

- <sup>1</sup> 来自`Areas`表的`AreaCode`列。
- <sup>2</sup> 来自`Interfaces`表的`InterfaceCode`列，留空即为使用KNX广播路由接口。

### 定时 Schedules

此表用于实现软件中的定时控制功能。

|列|类型|.NET类型|说明|
|---|---|---|---|
|ScheduleCode|Text|String|定时编号|
|ScheduleName|Text|String|定时名称|
|TargetType|Text|String|目标对象类型|
|TargetCode|Text|String|目标对象编号|
|ScheduleEvents|Text|String|定时事件|
|Enable|*Sequence*<sup>1</sup>|Boolean|启用|

- <sup>1</sup> FALSE/TRUE序列。

### 链接	Links

此表用于存储KNX系统中使用的链接（例如带有网页登录入口的KNX逻辑控制器）。

|列|类型|.NET类型|说明|
|---|---|---|---|
|LinkName|Text|String|链接名称|
|LinkUrl|Text|String|链接地址|
|Account|Text|String|账号名|
|Password|Text|String|密码|

## 图形文件格式 Graphics Format

本软件使用[draw.io](https://www.drawio.com)来绘制图形界面。

BedivereKnx.Client uses [draw.io](https://www.drawio.com) to draw graphical interfaces.

- （格式暂略，将在之后的版本中补充……）

## 鸣谢 Acknowledgments

非常感谢以下团体和个人对本项目的慷慨贡献和无私支持：

We would like to express our sincere gratitude to the following individuals and teams for their contributions and support to this software:
- [KNX Association cvba<br>](https://www.knx.org)
  KNX协会
- [ASTON Technologie GmbH<br>](https://www.aston-technologie.de/)
- [JGraph Ltd](https://www.drawio.com)
- [Embedded Systems, SIA](https://openrb.com/)
- [SIEMENS AG<br>](https://www.siemens.com)
  西门子股份公司
- [德力西集团有限公司](https://www.delixi.com/)
- [文心一言<br>](https://yiyan.baidu.com/)
  ERNIE Bot
- Anonym van Tadig
- 罗志坚

<br>

Copyright © 2024 Ouroboros Software Foundation
