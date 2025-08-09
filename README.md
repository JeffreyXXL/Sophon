# Sophon
# 项目介绍
本项目主要是用来进行个人软件开发学习用，通过开发的过程进行软件学习与巩固。
欢迎大家围观，如果看到错误的地方还请帮忙指正，无比感谢！

# 软件功能
主要功能为工业设备运动控制。包含以下功能：

| 序号 | 功能 | 备注 |
| --- | --- | --- |
| 1 | 硬件操作 | IO/伺服/其他硬件/通信协议 |
| 2 | 数据存储 | ORM |
| 3 | 工具库 | 容器（Autofac）/日志(NLog)/配置(JSON/XML/INI) |
| 4 | UI界面 | WPF/Prism |
| 5 | 流程管理 | 流程/状态机/事件总线 |

其他功能暂时边写边想，到时候有新的再往上面添加。

# 开发计划
阶段一：基础框架搭建

        1、解决方案搭建
                        2025/07/17 已完成
        2、IOC容器  使用autofac
                        2025/07/17 已完成
        3、日志系统 使用Nlog
                        2025/07/17 已完成
        4、配置管理 支持XML/JSON/INI等
                        2025/07/21 已完成
        5、编写测试用例并进行测试
                        2025/07/28 已完成

阶段二：流程与状态机

        1、流程与状态机
                        2025/08/08 已完成
        2、单步接口
                        2025/08/08 已完成
        3、事件总线
                        2025/08/09 已完成
阶段三：数据管理与存储

        1、ORM

        2、数据库搭建

阶段四：硬件部分代码开发

        1、轴卡 固高/雷赛/虚拟卡

        2、通讯协议 TCPIP/ModBus/EtherCAT

阶段五：UI层开发

        1、WPF学习 prism

        2、开发

这个就是一个大概的计划，具体的实施可能还需要在进行中来完善。所用的时间也没法预估，但是我自己也给自己定了另一个小目标，最好是能在今年之前完成的。

# 引用
当前软件中所有用到的第三方库都来自Nuget，Clone代码后可以直接在Nuget中还原。

| Nuget包 | 版本 | 主要使用项目 |
| --- | --- | --- |
| Autofac | 8.3.0 | Common |
| NLog | 6.0.1 | Common |
| NLog.config | 4.7.15 | Common |
| ini-parser | 2.5.5 | Common |
| Newtonsoft.Json | 13.0.3 | Common |
| ini-parser | 2.5.5 | Common |
| NUnit | 4.3.2 | Tests |
| NUnit3TestAdapter | 5.0.0 | Tests |
| Moq | 4.20.72 | Tests |


# 联系方式
| 方式 | 地址 |
| --- | --- |
| E-mail | jeffrey.xia@foxmail.com |
| 知乎 | https://www.zhihu.com/people/jeffrey-21-52 |
| 公众号 | <img width="235" height="82" alt="Snipaste_2025-08-08_13-50-59" src="https://github.com/user-attachments/assets/796ddc15-3282-4003-913a-e630993f364d" /> |
