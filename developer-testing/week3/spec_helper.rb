Spec::Matchers.define :be_of_size do |size_hash|
  match do |size_actual|
    (size_actual.width == size_hash[:width]) && (size_actual.height == size_hash[:height])
  end

  failure_message_for_should do |size_actual|
    "Wrong size, expected [width='#{size_hash[:width]}', height='#{size_hash[:height]}'], got #{size_actual.to_s}"
  end

  description do
    "Testing sizes"
  end
end

