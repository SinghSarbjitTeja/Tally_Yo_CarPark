import React, { Component } from 'react';

function PriceDisplay(props) {
    return (
        <div className="row">
            <div className="col-sm-12">
                <div className="jumbotron jumbotron-fluid">
                    <div className="container">
                        <form>
                            <div class="form-group row">
                                <label for="staticEmail" class="col-sm-2 col-form-label">NAME</label>
                                <div class="col-sm-10">
                                     {props.priceResult.name} 
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="colFormLabelLg" class="col-sm-2 col-form-label col-form-label-lg">Price</label>
                                <div class="col-sm-10">
                                   {props.priceResult.price}
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>


    )
};

export default PriceDisplay;