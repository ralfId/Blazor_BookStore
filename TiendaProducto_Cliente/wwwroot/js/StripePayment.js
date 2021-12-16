redirectToCheckout = function (sessionId) 
{
    var stripe = Stripe('pk_test_51K6iiqCyPItKy38zbUaY7f1OFXiB8DWonr93MtIKeUUuoVQaIrxFaogiWIJYTlDGHMFaRogEpf2Wd9mZuvIH2oCu002JzgkQw9');

    stripe.redirectToCheckout({ sessionId: sessionId });

}