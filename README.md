# **Hệ Thống Bán Nước Hoa Trực Tuyến**
## Mô Tả
Hệ thống **Bán Nước Hoa Trực Tuyến** là một ứng dụng web full-stack được phát triển bằng C# và ASP.NET Core cho backend, với HTML/CSS/JavaScript cho frontend. Hệ thống này cho phép người dùng tìm kiếm, lọc các sản phẩm nước hoa theo thương hiệu, loại mùi hương, giá cả và thực hiện mua hàng một cách dễ dàng. Ngoài ra, trang web cung cấp tính năng quản lý sản phẩm và đơn hàng cho quản trị viên.

- **Động lực:** Việc xây dựng website này xuất phát từ nhu cầu mua sắm trực tuyến ngày càng tăng của người tiêu dùng, đặc biệt là trong lĩnh vực nước hoa cao cấp.
- **Tại sao phát triển?** Hệ thống được xây dựng để cung cấp một trải nghiệm mua sắm nước hoa trực tuyến dễ dàng và hiện đại, giúp khách hàng tiếp cận với nhiều sản phẩm và thương hiệu một cách thuận tiện.
- **Vấn đề giải quyết:** Hệ thống giúp giải quyết vấn đề về tìm kiếm sản phẩm nhanh chóng, quá trình thanh toán an toàn và khả năng quản lý sản phẩm hiệu quả cho quản trị viên.
- **Bài học:** Qua dự án này, tôi đã học được cách xây dựng hệ thống mua sắm trực tuyến bằng công nghệ C#, xử lý các quy trình bán hàng, quản lý giỏ hàng, và tích hợp các phương thức thanh toán.
---
## Mục Lục
- [Cài Đặt](#cai dat)
- [Cách Sử Dụng](#cach su dung)
- [Người Thực Hiện](#nguoi thuc hien)
- [Tính Năng](#tinh nang)
---
## Cài Đặt
### Backend (ASP.NET Core)
#### Yêu Cầu
- .NET 6 trở lên
- SQL Server (hoặc bất kỳ cơ sở dữ liệu tương thích nào)
#### Các Bước
1. Clone repository từ GitHub và mở trong Visual Studio.
2. Cài đặt các gói nuget cần thiết cho dự án bằng lệnh dotnet restore.
3. Cấu hình chuỗi kết nối đến cơ sở dữ liệu trong tệp appsettings.json.
4. Build và chạy dự án bằng cách chọn tùy chọn Run trong Visual Studio.
### Frontend (HTML/CSS/JavaScript)
#### Yêu Cầu
- Visual Studio Code (hoặc bất kỳ editor nào bạn thích)
- Trình duyệt hỗ trợ HTML5
#### Các Bước
1. Mở thư mục frontend trong editor của bạn.
2. Đảm bảo rằng các file HTML/CSS/JavaScript được kết nối đúng cách với API backend.
3. Chạy ứng dụng trực tiếp trên trình duyệt bằng cách mở tệp HTML chính.
---
## Cách Sử Dụng
Sau khi cài đặt và chạy thành công cả backend và frontend, hệ thống có thể truy cập qua trình duyệt tại địa chỉ http://localhost:5000. Dưới đây là một số hình ảnh của hệ thống:

Trang Chính:



Danh Mục Sản Phẩm:



Tìm Kiếm và Lọc Sản Phẩm:



Chi Tiết Sản Phẩm:



Giỏ Hàng:



Thanh Toán:



Quản Lý Đơn Hàng:



Trang Quản Trị:


---
## Người thực hiện
- **[Nguyễn Đình Tuấn](https://github.com/TunNeeeee) - Trưởng Nhóm/Fullstack
- **Nguyễn Quốc Cường** - Frontend
- **Nguyễn Hoàng Huy** - Frontend
---
## Tính Năng
Đăng nhập/Đăng xuất bằng OAuth2 với Google/Facebook
Xác thực JWT
Tìm kiếm sản phẩm nước hoa và lọc theo thương hiệu, loại mùi hương, giá cả
Giỏ hàng và thanh toán bảo mật
Quản lý đơn hàng và sản phẩm cho quản trị viên
Tích hợp phương thức thanh toán qua các cổng thanh toán trực tuyến
Thiết kế đáp ứng trên cả thiết bị di động và máy tính
