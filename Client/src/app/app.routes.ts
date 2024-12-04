import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ShopComponent } from './features/shop/shop.component';
import { ProductDetailsComponent } from './features/shop/product-details/product-details.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { ServerErrorComponent } from './shared/components/server-error/server-error.component';
import { CartComponent } from './features/cart/cart.component';
import { CheckoutComponent } from './features/checkout/checkout.component';
import { LoginComponent } from './features/account/login/login.component';
import { RegisterComponent } from './features/account/register/register.component';
import { authGuard } from './core/guards/auth.guard';
import { emptyCartGuard } from './core/guards/empty-cart.guard';
import { CheckoutSuccessComponent } from './features/checkout/checkout-success/checkout-success.component';
import { OrderDetailedComponent } from './features/orders/order-detailed/order-detailed.component';
import { OrderComponent } from './features/orders/order.component';
import { orderCompleteGuard } from './core/guards/order-complete.guard';
import { AdminComponent } from './features/admin/admin.component';
import { adminGuard } from './core/guards/admin.guard';

export const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'shop', component: ShopComponent},
  {path: 'shop/:id', component: ProductDetailsComponent},
  {path: 'cart', component: CartComponent},
  {path: 'checkout', loadChildren: () => import('./features/checkout/routes')
      .then(r => r.checkoutRoutes)},
  {path: 'orders', loadChildren: () => import('./features/orders/routes')
      .then(r => r.orderRoutes)},
  {path: 'account', loadChildren: () => import('./features/account/routes')
      .then(r => r.accountRoutes)},
  
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: 'admin', loadComponent: () => import('./features/admin/admin.component')
      .then(c => c.AdminComponent), canActivate: [authGuard, adminGuard]},
  {path: '**', redirectTo: 'not-found', pathMatch: 'full'},
];
