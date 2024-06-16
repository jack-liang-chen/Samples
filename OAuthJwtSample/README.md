# 彻底理解 OAuth2 协议
https://www.bilibili.com/video/BV1zt41127hX/?spm_id_from=333.337.search-card.all.click&vd_source=ea97dd6e2e4edba7396c40b1012e7026

AccessToken: 是一种短期令牌，用于访问受保护的资源。通常有一个较短的有效期（例如，几分钟到几小时）。

RefreshToken: 是一种长期令牌，用于获取新的AccessToken。当AccessToken过期时，
客户端可以使用RefreshToken获取新的AccessToken，而不需要再次进行用户身份验证。
RefreshToken通常有一个较长的有效期（例如，几天到几个月）。

在身份认证和授权过程中，Claim（声明）是一种表示用户身份属性的键值对。
它可以包含关于用户的各种信息，如用户名、用户ID、角色、权限等。Claim 是JWT（JSON Web Token）和许多其他安全令牌标准的重要组成部分，
用于传递用户身份和其他元数据。


