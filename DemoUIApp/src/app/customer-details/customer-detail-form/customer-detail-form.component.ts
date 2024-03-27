import { Component } from '@angular/core';
import { CustomerDetailService } from '../../shared/customer-detail.service';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-customer-detail-form',
  templateUrl: './customer-detail-form.component.html',
  styleUrl: './customer-detail-form.component.css'
})
export class CustomerDetailFormComponent {
 
  customerID! : number;
  isFormSubmitted : boolean = false;

  constructor(public service: CustomerDetailService, private toastr : ToastrService, private route : ActivatedRoute, private router : Router){

  }

  ngOnInit(){
    this.customerID = this.route.snapshot.params['customerID'];
    if (this.customerID != null){
      this.service.getCustomerByID(this.customerID);
    }
  }

  onSubmit(form: NgForm){
    this.isFormSubmitted = true;
    if (form.valid){
      if (this.customerID != null){
        this.service.putCustomer()
        .subscribe({
          next : res => {
            console.log(res);
            this.resetForm(form);
            this.toastr.success("Customer updated successsfully","Customer Details Form");
            this.router.navigateByUrl("customer");
          },
          error : err => {console.log(err)}
        })
      }
      else{
        this.service.postCustomer()
        .subscribe({
          next : res => {
            console.log(res);
            this.resetForm(form);
            this.toastr.success("Customer added successsfully","Customer Details Form");
            this.router.navigateByUrl("customer");
          },
          error : err => {console.log(err)}
        })
      }

    }

  }

  resetForm(form :NgForm){
    form.form.reset();
  }

  backtoCustomers(){
    this.router.navigateByUrl("customer");
  }
}
