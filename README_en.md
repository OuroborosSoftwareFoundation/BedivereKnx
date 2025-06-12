# Bedivere Knx Solution

[中文readme文档](README.md)

KNX intelligent lighting control software, based on Knx.Falcon.Sdk library.

> [!CAUTION]
> This software may have bugs and is not recommended for commercial use, otherwise the consequences will be borne by oneself.

## License

This program Is free software: you can redistribute it And/Or modify it under the terms Of the GNU General Public License As published by the Free Software Foundation, either version 3 Of the License, Or (at your option) any later version.

This program Is distributed In the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty Of MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License For more details.

You should have received a copy Of the GNU General Public License along with this program. If Not, see [http://www.gnu.org/licenses/](http://www.gnu.org/licenses/).

## Authorization

This software is authorized with `Ouroboros.AuthMnager.Eos`. If you don't need authorization , you can simply delete the relevant code and compile it.

## Data File Format

BedivereKnx.Client uses Excel to store data. The following provides a detailed description of the data format (.NET Type with underline is from Knx.Falcon library):

### Interfaces

This sheet stores interfaces information. Left blank for default broadcast routing interface `224.0.23.12:3671`.

| Column           | Type                             | .NET Type                            | Caption           |
| ---------------- | -------------------------------- | ------------------------------------ | ----------------- |
| InterfaceCode    | Text                             | String                               | Interface Code    |
| InterfaceName    | Text                             | String                               | Interface Name    |
| InterfaceType    | *Sequence*<sup>1</sup>           | <ins>*ConnectorType*</ins>           | Interface Type    |
| InterfaceAddress | Text                             | String                               | Interface Address |
| Port             | Number                           | Integer                              | Port              |
| Enable           | *Sequence*<sup>2</sup>           | Boolean                              | Enable            |

- <sup>1</sup> KNX Interface Type, IpRouting、IpTunneling、Usb.
- <sup>2</sup> FALSE/TRUE Sequence.

### Areas

This sheet stores KNX area information.

| Column     | Type                        | .NET Type | Caption          |
| ---------- | --------------------------- | --------- | ---------------- |
| MainCode   | Text                        | String    | Main Area Code   |
| MainArea   | Text                        | String    | Main Area Name   |
| MiddleCode | Text                        | String    | Middle Area Code |
| MiddleArea | Text                        | String    | Middle Area Name |
| SubCode    | Text                        | String    | Sub Area Code    |
| SubArea    | Text                        | String    | Sub Area Name    |
| AreaCode   | General<sup>1</sup>         | String    | Area Code        |

- <sup>1</sup> AreaCode column is filled with formula, its format is `MainCode.MiddleCode.SubCode`.

### Objects

This sheet stores objects information, each object has 4 groups: switch control, switch feedback, value control, and value feedback.

| Column                            | Type                             | .NET Type                            | Caption                 |
| --------------------------------- | -------------------------------- | ------------------------------------ | ----------------------- |
| AreaCode<sup>1</sup>              | Text                             | String                               | Area Code               |
| InterfaceCode<sup>2</sup>         | Text                             | String                               | Interface Code          |
| ObjectCode                        | Text                             | String                               | Object Code             |
| ObjectType                        | *Sequence*                       | *KnxObjectType*<sup>3</sup>           | Object Type             |
| ObjectName                        | Text                             | String                               | Object Name             |
| Sw_GrpDpt                         | *Sequence*<sup>4</sup>           | <ins>*DptBase*</ins>                 | Switch Group DPT        |
| Sw_Ctl_GrpAddr                    | Text                             | <ins>*GroupAddress*</ins>            | Switch Control Address  |
| Sw_Fdb_GrpAddr                    | Text                             | <ins>*GroupAddress*</ins>            | Switch Feedback Address |
| Val_GrpDpt                        | *Sequence*<sup>4</sup>           | *DptBase*                            | Value Group DPT         |
| Val_Ctl_GrpAddr                   | Text                             | <ins>*GroupAddress*</ins>            | Value Control Address   |
| Val_Fdb_GrpAddr                   | Text                             | <ins>*GroupAddress*</ins>            | Value Feedback Address  |

- <sup>1</sup> From column `AreaCode` of sheet `Areas`.
- <sup>2</sup> From column `InterfaceCode` of sheet `Interfaces`, left blank for default broadcast routing interface.
- <sup>3</sup> *KnxObjectType* A is an enumeration type defined by BedivereKnx, representing the KNX object type (Switch/Dimming/Scene/...).
- <sup>4</sup> KNX DataPointType sequence, from `Knx.Falcon.ApplicationData.DatapointTypes.DptFactory.AllDatapointTypes`.

### Scenes

This sheet stores scenes information.

| Column                            | Type | .NET Type                           | Caption       |
| --------------------------------- | ---- | ----------------------------------- | ------------- |
| AreaCode<sup>1</sup>              | Text | String                              | Area Code     |
| InterfaceCode<sup>2</sup>         | Text | String                              | Object Type   |
| SceneCode                         | Text | String                              | Scene Code    |
| SceneName                         | Text | String                              | Scene Name    |
| GroupAddress                      | Text | <ins>*GroupAddress*</ins>           | Scene Address |
| SceneValues<sup>3</sup>           | Text | String                              | Scene Values  |

- <sup>1</sup> From column `AreaCode` of sheet `Areas`.
- <sup>2</sup> From column `InterfaceCode` of sheet `Interfaces`, left blank for default broadcast routing interface.
- <sup>3</sup> Format is `{SceneValue1}={SceneName1},{SceneValue2}={SceneName2},……`, for example: `0=All Off,1=All On`.

### Devices

This table stores devices information in the KNX system, used to check online status of devices.

| Column                            | Type | .NET Type                                | Caption            |
| --------------------------------- | ---- | ---------------------------------------- | ------------------ |
| AreaCode<sup>1</sup>              | Text | String                                   | Area Code          |
| InterfaceCode<sup>2</sup>         | Text | String                                   | Object Type        |
| DeviceCode                        | Text | String                                   | Device Code        |
| DeviceName                        | Text | String                                   | Device Name        |
| DeviceModel                       | Text | String                                   | Device Model       |
| IndividualAddress                 | Text | <ins>*IndividualAddress*</ins>           | Individual Address |

- <sup>1</sup> From column `AreaCode` of sheet `Areas`.
- <sup>2</sup> From column `InterfaceCode` of sheet `Interfaces`, left blank for default broadcast routing interface.

### Schedules

This sheet is used to implement the software timing control function.

| Column         | Type                             | .NET Type | Caption         |
| -------------- | -------------------------------- | --------- | --------------- |
| ScheduleCode   | Text                             | String    | Schedule Code   |
| ScheduleName   | Text                             | String    | Schedule Name   |
| TargetType     | Text                             | String    | Target Type     |
| TargetCode     | Text                             | String    | Target Code     |
| ScheduleEvents | Text                             | String    | Schedule Events |
| Enable         | *Sequence*<sup>1</sup>           | Boolean   | Enable          |

- <sup>1</sup> FALSE/TRUE Sequence.

### Links

This sheet is used to store links of the KNX system (such as KNX logic controllers with web login entrances).

| Column   | Type | .NET Type | Caption  |
| -------- | ---- | --------- | -------- |
| LinkName | Text | String    | LinkName |
| LinkUrl  | Text | String    | LinkUrl  |
| Account  | Text | String    | Account  |
| Password | Text | String    | Password |

## Graphics Format

BedivereKnx.Client uses [draw.io](https://www.drawio.com) to draw graphical interfaces.

### Mapping String Format

`{Mapping Object}{Direction Symbol}{Change Values}{Data Type}`.

#### Mapping Object

- From group address: Use the group address, such as `1/2/101`.
- From datatable: Format is `${ObjectCode}`, ObjectCode is from Objects table or Scene table.

#### Direction Symbol

This and subsequent items can be omitted, its default value is  `@0|1#1.000`.

- `@` for feedback
- `=` for control

#### Change Values

Supports 3 methods: fixed value, values toggle, and range values. This and subsequent items can be omitted, its default value is `0|1#1.000`.

- Fixed value: Input the value directly.
- Values toggle: Use `|` to split the values.
- Ranged values: Format is `{Min Value}~{Max Value}`.

#### Data Type

Data type refers to DPT, preceded by `#`, such as`#1.001`、`#5.003`, etc. This item can be omitted, the default value for dialog is `1.000` (Boolean), and the default value for analog is `5.000`(8-bit unsigned integer).

#### Examples

- `1/1/101@0|1#1.0`: feedback of group address 1/1/101, DPT is 1.000, changes between 0 and 1.
- `1/1/102`: feedback of group address 1/1/102, DPT is 1.000, changes between 0 and 1.
- `1/1/1=1`: control of group address 1/1/1, DPT is 1.000, control value is 1.
- `1/2/200=5`: control of group address 1/2/200, DPT is 5.000, control value is 5.
- `1/2/201@0~100#5.003`: feedback of group address 1/2/201, DPTis 5.003, changes between 0 and 100.
- ...

## USB Interface Connect Exception

When connecting the USB interface, there may be an error exception "KnxUsbFix not installed", install KnxUsbFix to fix it.

For details, please refer to: https://support.knx.org/hc/en-us/articles/115001443670-KNX-USB-interface-not-recognized

## Reference

- [Knx.Falcon.Sdk](https://help.knx.org/falconsdk)
- [DocumentFormat.OpenXml](https://github.com/dotnet/Open-XML-SDK)
- [Ouroboros.Hmi]
- [Ouroboros.Authorization]

## Acknowledgments

We would like to express our sincere gratitude to the following individuals and teams for their contributions and support to this software:

- [KNX Association cvba](https://www.knx.org)
- [ASTON Technologie GmbH](https://www.aston-technologie.de/)
- [JGraph Ltd](https://www.drawio.com)
- [Embedded Systems, SIA](https://openrb.com/)
- [SIEMENS AG](https://www.siemens.com)
- [Delixi Electric](https://www.delixi-electric.com/en/)
- [ERNIE Bot](https://yiyan.baidu.com/)
- WeChat Official Account: 技术老小子
- Anonym van Tadig
- Luo Zhijian

<br>

Copyright © 2024 Ouroboros Software Foundation. All rights reserved.
