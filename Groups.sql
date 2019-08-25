insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'47B974F0-00CC-415B-AF2A-93F010491212', 'Saude', NULL, NULL, NULL)

--select * from [Group] where ParentId = '47B974F0-00CC-415B-AF2A-93F010491212'

insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'BFDF8F5E-C514-416D-B6B0-35B196F5CA96', 'Peso', NULL, '47B974F0-00CC-415B-AF2A-93F010491212', NULL)
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'3BBF57E4-4A32-4590-BBDC-70510109ECEB', 'Sono', NULL, '47B974F0-00CC-415B-AF2A-93F010491212', NULL)
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'4665DB6B-38B5-4F19-9833-A8AD61DB1587', 'Local', NULL, '47B974F0-00CC-415B-AF2A-93F010491212', NULL)
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'3A6EFE92-00A4-4B1B-BC40-C5B2C83CF0C1', 'Cardio', NULL, '47B974F0-00CC-415B-AF2A-93F010491212', NULL)

----CARDIO
--select * from [Group] where ParentId = '3A6EFE92-00A4-4B1B-BC40-C5B2C83CF0C1'

insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'C0EFE267-E8ED-4B79-A125-DB15ABC0780D', 'Frequência', NULL, '3A6EFE92-00A4-4B1B-BC40-C5B2C83CF0C1', 'BPM')

----SONO
--select * from [Group] where ParentId = '3BBF57E4-4A32-4590-BBDC-70510109ECEB'

insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'D28289C7-C532-4345-8847-247DE09F406E', 'Total', NULL, '3BBF57E4-4A32-4590-BBDC-70510109ECEB', NULL)
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'1E2E19A7-23B6-4DAE-8439-4F1F8D77514C', 'Stage', NULL, '3BBF57E4-4A32-4590-BBDC-70510109ECEB', NULL)
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'238A6AF0-8FAA-4228-8531-6E83D83C879B', 'Classic', NULL, '3BBF57E4-4A32-4590-BBDC-70510109ECEB', NULL)
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', 'Sumary', NULL, '3BBF57E4-4A32-4590-BBDC-70510109ECEB', NULL)

--select * from [Group] where ParentId = 'D28289C7-C532-4345-8847-247DE09F406E'

insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'08BC140E-7D67-4283-A9EA-02D5F468AA24', 'TimeInBed', NULL, 'D28289C7-C532-4345-8847-247DE09F406E', 'min')
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'F1725DF1-1D22-4CD7-94F9-8FDD26FA4965', 'Asleep', NULL, 'D28289C7-C532-4345-8847-247DE09F406E', 'min')
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'6497347A-22BA-44D7-B500-9B3967293E18', 'Efficiency', NULL, 'D28289C7-C532-4345-8847-247DE09F406E', NULL)
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'6F8BB4C6-7420-4CA5-B029-B0F58D794C5D', 'Duration', NULL, 'D28289C7-C532-4345-8847-247DE09F406E', 'ms')
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'E0B0A866-AA36-4597-B1D9-C94F7825DDE3', 'FallAsleep', NULL, 'D28289C7-C532-4345-8847-247DE09F406E', 'min')
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'D33348BA-D90A-4D38-A600-E1971131AFA7', 'AfterWakeup', NULL, 'D28289C7-C532-4345-8847-247DE09F406E', 'min')
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'0C209B5D-7F7A-46D8-99A0-FACE6B280E68', 'Awake', NULL, 'D28289C7-C532-4345-8847-247DE09F406E', 'min')

--select * from [Group] where ParentId = '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3'

insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'88E463A0-46CF-4D8C-9BAD-0CC5A1C4ABD3', 'Awake', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', 'min')
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'46CA76B0-1724-4B86-B369-1B5D87446ECC', 'REMCount', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', NULL)
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'B837B0A9-DB6E-4321-B817-277ABBCB4B51', 'Wake', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', 'min')
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'0F97B7A9-7C66-439E-9A93-28BE0F48FD6A', 'AwakeCount', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', NULL)
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'F95BE23B-406F-4A89-B838-317F4B154A42', 'RestlessCount', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', NULL)
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'4ECE3413-517D-4F39-A764-3B07FA3EDA47', 'LightCount', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', NULL)
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'DB707970-DBE6-4752-8523-3CF918D34084', 'REM', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', 'min')
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'5AC43A3A-C66C-459C-BB16-6D66B9B9DC9D', 'DeepCount', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', NULL)
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'1C430C3C-CC9F-4FD6-8A04-762CDCDCADC4', 'WakeCount', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', NULL)
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'FB3A393B-1AAF-4047-ACC1-7A1D6D3AF6CE', 'Deep', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', 'min')
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'7B6136C6-03CD-47B6-A668-A6A49D48824E', 'Restless', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', 'min')
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'EE8BC452-A04A-4320-BBFC-BB66384E0253', 'Light', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', 'min')
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'AB0DEE1F-73E8-4160-9BE7-DD43560567D5', 'Asleep', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', 'min')
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'523C5B9E-EFF7-4AD3-8DF6-FCC00FDB2D9E', 'AsleepCount', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', NULL)

insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'D28DD178-0BC9-4003-BF3F-030C37E541D2', 'WakeThirtyDayAvgMinutes', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', NULL)
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'6135B6E7-BB9D-46D5-97B1-1335F692DD51', 'DeepThirtyDayAvgMinutes', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', NULL)
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'EBC868AE-566F-4E66-B86A-6A995ADD072E', 'REMThirtyDayAvgMinutes', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', NULL)
insert into [Group] (Id, Name, Initials, ParentId, MeasureUnit) VALUES (
'09B481BC-1412-4B38-9C70-BDA801FC5865', 'LightThirtyDayAvgMinutes', NULL, '8EC4BC4F-B0FC-4C17-8EC5-E9B964450DE3', NULL)
